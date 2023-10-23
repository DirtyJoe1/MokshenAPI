using Entities;
using Entities.Models;
using Interfaces;
using LoggerService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace MokshenAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) => services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
        });
        public static void ConfigureIISIntegration(this IServiceCollection services) => services.Configure<IISOptions>(options =>{});
        public static void ConfigureLoggerService(this IServiceCollection services) => services.AddScoped<ILoggerManager, LoggerManager>();
        //public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) => services
        //    .AddDbContext<RepositoryContext>(opts =>
        //    opts.UseSqlServer(configuration.GetConnectionString("sqlConnection"),
        //        b => b.MigrationsAssembly("MokshenAPI")));
        //public static IMvcBuilder AddCustomCSVFormatter(this IMvcBuilder builder) => builder.AddMvcOptions(config => config.OutputFormatters.Add(new CsvOutputFormatter()));
        //public static void ConfigureIdentity(this IServiceCollection services)
        //{
        //    var builder = services.AddIdentityCore<User>(o =>
        //    {
        //        o.Password.RequireDigit = true;
        //        o.Password.RequireLowercase = false;
        //        o.Password.RequireUppercase = false;
        //        o.Password.RequireNonAlphanumeric = false;
        //        o.Password.RequiredLength = 10;
        //        o.User.RequireUniqueEmail = true;
        //    });
        //    builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole),
        //    builder.Services);
        //    builder.AddEntityFrameworkStores<RepositoryContext>().AddDefaultTokenProviders();
        //}
        //public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        //{
        //    var jwtSettings = configuration.GetSection("JwtSettings");
        //    var secretKey = Environment.GetEnvironmentVariable("SECRET");
        //    services.AddAuthentication(opt =>
        //    {
        //        opt.DefaultAuthenticateScheme =
        //       JwtBearerDefaults.AuthenticationScheme;
        //        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //    })
        //    .AddJwtBearer(options =>
        //    {
        //        options.TokenValidationParameters = new TokenValidationParameters
        //        {
        //            ValidateIssuer = true,
        //            ValidateAudience = true,
        //            ValidateLifetime = true,
        //            ValidateIssuerSigningKey = true,
        //            ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
        //            ValidAudience = jwtSettings.GetSection("validAudience").Value,
        //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        //        };
        //    });
        //}
        //public static void ConfigureSwagger(this IServiceCollection services)
        //{
        //    services.AddSwaggerGen(s =>
        //    {
        //        s.SwaggerDoc("v1", new OpenApiInfo
        //        {
        //            Title = "Mokshen API",
        //            Version = "v1",
        //            Description = "Mokshen API by Ruslan Ovtin",
        //            TermsOfService = new Uri("https://example.com/terms"),
        //            Contact = new OpenApiContact
        //            {
        //                Name = "Ruslan Ovtin",
        //                Email = "Contact.rusov@mail.ru",
        //                Url = new Uri("https://vk.com/notluckyatall"),
        //            },
        //            License = new OpenApiLicense
        //            {
        //                Name = "Mokshen API LICX",
        //                Url = new Uri("https://example.com/license"),
        //            }
        //        });
        //        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        //        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        //        s.IncludeXmlComments(xmlPath);
        //        s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        //        {
        //            In = ParameterLocation.Header,
        //            Description = "Place to add JWT with Bearer",
        //            Name = "Authorization",
        //            Type = SecuritySchemeType.ApiKey,
        //            Scheme = "Bearer"
        //        });
        //        s.AddSecurityRequirement(new OpenApiSecurityRequirement()
        //        {
        //            {
        //                new OpenApiSecurityScheme
        //                {
        //                    Reference = new OpenApiReference
        //                    {
        //                        Type = ReferenceType.SecurityScheme,
        //                        Id = "Bearer"
        //                    },
        //                    Name = "Bearer",
        //                },
        //                new List<string>()
        //            }
        //        });
        //    });
        //}
    }
}
