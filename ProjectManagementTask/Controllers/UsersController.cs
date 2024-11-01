using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using ProjectManagement.Dtos;
using ProjectManagement.Errors;
using ProjectManagement.Helpers;
using ProjectManagement.Models;
using ProjectManagement.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace ProjectManagementTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly JWTSettings _jWTSettings;
        public UsersController(
            
            IMapper mapper,
            UserManager<User> userManager,
            JWTSettings jWTSettings)
        {
         
            _mapper = mapper;
            _userManager = userManager;
            _jWTSettings = jWTSettings;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginDto dto)
        {
            var user =_userManager.Users.Include(u=>u.UserRoles).
                SingleOrDefault(e => e.Email == dto.Email);

            if (user == null)
                return BadRequest(new ApiResponse(404, "Sorry,user not found"));

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!userHasValidPassword)
                return BadRequest(new ApiResponse(404, "Sorry,incrorrect password"));

            return GetUserToken(user);

        }

        private IActionResult GetUserToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jWTSettings.Secret);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            };


            foreach (var role in user.UserRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role,role.ToString()));
            }


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_jWTSettings.TokenLifeTime),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var vm = new LoginDataDto()
            {
                Token = tokenHandler.WriteToken(token),
                User = _mapper.Map<UserDto>(user)
            };

            return Ok(vm);

        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(AddUserDto dto)
        {

            if (_userManager.Users.Any(u=>u.Email == dto.Email || u.UserName == dto.UserName))
                return BadRequest(new ApiResponse(400, "Sorry,user is allready exist"));


            var user = _mapper.Map<User>(dto);
            var identityResult = await _userManager.CreateAsync(user, dto.Password);
            if (!identityResult.Succeeded)
                return BadRequest(new ApiResponse(400, "Sorry,user not added "));


            var roleResult = await _userManager.AddToRoleAsync(user, dto.Role.ToString());

            if (!roleResult.Succeeded)
                return BadRequest(new ApiResponse(400, "Sorry,roles not added"));

            var vm = new { user = _mapper.Map<UserDto>(user) };

            return Ok(vm);
        }
    }
}
