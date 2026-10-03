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
using Sello.Api.ExceptionHandling;

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
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IShippingAddressService, ShippingAddressService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDashboardService, DashboardService>();


            //repositories
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IShippingAddressRepository, ShippingAddressRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IUserRepository, UserRepository>();


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

            services.AddExceptionHandler(options => { });
            services.AddExceptionHandler<GlobalExceptionHandler>();

            return services;
        }
    }
}
