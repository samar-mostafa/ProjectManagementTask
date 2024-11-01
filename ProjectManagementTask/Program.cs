using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Configurations;
using ProjectManagement.Helpers;
using ProjectManagement.Models;
using ProjectManagement.Seeds;
using ProjectManagement.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ProjectManagement.Data.DbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddGenericServices();
builder.Services.AddRepositories();
builder.Services.AddIdentity<User, Role>(setup =>
{
    setup.Password.RequireDigit = false;
    setup.Password.RequireLowercase = false;
    setup.Password.RequireNonAlphanumeric = false;
    setup.Password.RequireUppercase = false;
    setup.Password.RequiredUniqueChars = 0;
    setup.Password.RequiredLength = 3;

    setup.User.RequireUniqueEmail = true;

    setup.Lockout.AllowedForNewUsers = false;
    setup.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
})
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ProjectManagement.Data.DbContext>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(DbContextProfile).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();
var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
var scope = scopeFactory.CreateScope();
var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

await DefaultRoles.SeedAsync(roleManager);

app.MapControllers();

app.Run();
