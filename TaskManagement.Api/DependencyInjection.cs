using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SurveyBasket.Api.Authentications;
using System.Reflection;
using System.Text;
using TaskManagement.Api.Entities;
using TaskManagement.Api.Persistence;
using TaskManagement.Api.Services;
//using TaskManagement.Api.Services;

namespace TaskManagement.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services,IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") ??
            throw new InvalidOperationException("Connection string 'DefaultConnection' not found");

        services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));
        services.AddAuthConfig(configuration);

        services.AddControllers();
        services.AddOpenApi();
        services
            .AddMapsterConfig();

        services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
    private static IServiceCollection AddMapsterConfig(this IServiceCollection services)
    {
        //Add Mapster
        var mappingConfig = TypeAdapterConfig.GlobalSettings;
        mappingConfig.Scan(Assembly.GetExecutingAssembly());
        services.AddSingleton(new Mapper(mappingConfig));

        return services;
    }

    private static IServiceCollection AddAuthConfig(this IServiceCollection services,IConfiguration configuration)
    {
        var jwtSetting = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();

        //services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));

        services.AddOptions<JwtOptions>()
            .BindConfiguration(JwtOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton<IJwtProvider, JwtProvider>();


        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(o =>
        {
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting?.Key!)),
                ValidIssuer = jwtSetting?.Issuer,
                ValidAudience = jwtSetting?.Audience

            };
        });

        //services.Configure<IdentityOptions>(options =>
        //{
        //    // Default User settings.

        //    options.SignIn.RequireConfirmedEmail = true;
        //    options.Password.RequiredLength = 6;

        //    options.User.RequireUniqueEmail = false;

        //});

        return services;
    }
}
