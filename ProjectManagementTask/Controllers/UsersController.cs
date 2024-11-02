
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectManagement.Dtos;
using ProjectManagement.Errors;
using ProjectManagement.Helpers;
using ProjectManagement.Models;
using ProjectManagement.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace ProjectManagementTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IMapper mapper, UserManager<User> userManager,
        SignInManager<User> signInManager, RoleManager<Role> roleManager,
        JWTSettings jWtSettings) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginDto dto)
        {
            var user = userManager.Users.Include(u => u.UserRoles)
                .SingleOrDefault(e => e.Email == dto.Email);
            if (user == null) return BadRequest(new ApiResponse(404, "Sorry, user not found"));
            var userHasValidPassword = await userManager.CheckPasswordAsync(user, dto.Password);
            if (!userHasValidPassword) return
                    BadRequest(new ApiResponse(404, "Sorry, incorrect password"));
            var userSignedIn = await signInManager.
                PasswordSignInAsync(user.UserName!, dto.Password, false, false);
            return await GetUserToken(user);
        }
        private async Task<IActionResult> GetUserToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jWtSettings.Secret);
            if (user.Email != null)
            {
                var claims = new List<Claim>
            { new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            };
                foreach (var userRole in user.UserRoles)
                {
                    var role = await roleManager.FindByIdAsync(userRole.RoleId.ToString());
                    if (role is { Name: not null }) claims.Add(new Claim(ClaimTypes.Role, role.Name));
                }
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.Add(jWtSettings.TokenLifeTime),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var vm = new LoginDataDto()
                {
                    Token = tokenHandler.WriteToken(token),
                    User = mapper.Map<UserDto>(user)
                };
                return Ok(vm);
            }
            else
            {
                return BadRequest(new ApiResponse(404, "Sorry, user not found"));
            }
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(AddUserDto dto)
        {
            if (userManager.Users.Any(u => u.Email == dto.Email || u.UserName == dto.UserName))
                return BadRequest(new ApiResponse(400, "Sorry, user already exists"));
            var user = mapper.Map<User>(dto);
            var identityResult = await userManager.CreateAsync(user, dto.Password);
            if (!identityResult.Succeeded)
                return BadRequest(new ApiResponse(400, "Sorry, user not added"));
            var roleResult = await userManager.AddToRoleAsync(user, dto.Role.ToString());
            if (!roleResult.Succeeded)
                return BadRequest(new ApiResponse(400, "Sorry, roles not added"));
            return Ok();
        }
    }
}