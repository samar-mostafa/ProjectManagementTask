using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProjectManagement.Configurations;
using ProjectManagement.Helpers;
using ProjectManagement.Models;
using ProjectManagement.Seeds;
using ProjectManagement.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ProjectManagement.Data.DbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddGenericServices();
builder.Services.AddRepositories();
var jwtSettings = new JWTSettings();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Project management", Version = "v1" });

    // Configure JWT Authentication for Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\""
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
    };
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("EmployeePolicy", policy =>
            policy.RequireRole(AppRoles.Employee));
    });}
);
builder.Services.AddSingleton<JWTSettings>
    (sp => sp.GetRequiredService<IOptions<JWTSettings>>().Value);
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

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
var scope = scopeFactory.CreateScope();
var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

await DefaultRoles.SeedAsync(roleManager);

app.UseEndpoints(endpoints =>endpoints.MapControllers());


app.Run();
