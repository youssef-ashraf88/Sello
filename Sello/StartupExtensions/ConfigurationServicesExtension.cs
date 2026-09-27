using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sello.Application.ServiceContracts;
using Sello.Application.Services;
using Sello.Domain.Entities.Identity;
using Sello.Infrastructure.Data;
using Sello.Application.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Sello.Application.Mappings;
using Sello.Domain.RepositoryContracts;
using Sello.Infrastructure.Repositories;
using FluentValidation;
using Sello.Application.Validators;
using FluentValidation.AspNetCore;

namespace Sello.Api.StartupExtensions
{
    public static class ConfigurationServicesExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(
                configuration.GetSection("Jwt"));

            var jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>();

            services.AddValidatorsFromAssemblyContaining<LoginDtoValidator>();
            services.AddFluentValidationAutoValidation();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddHttpContextAccessor();

            //services
            services.AddTransient<IJwtService, JwtService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();


            //repositories
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();


            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                options.Lockout.AllowedForNewUsers = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidAudience = jwtSettings!.Audience,
                        ValidateIssuer = true,
                        ValidIssuer = jwtSettings!.Issuer,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key!))
                    };
                });


            services.AddAuthorization();


            services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

            return services;
        }
    }
}
