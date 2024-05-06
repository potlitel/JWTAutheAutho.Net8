using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Reflection;

namespace JWTLoginSystem.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// AddCorsService method: Add configurations for the Cors service.
        /// </summary>
        /// <param name="services">The services</param>
        /// <returns></returns>
        public static IServiceCollection AddCorsService(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(
                    name: "develop",
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .WithExposedHeaders("*");
                    }
                );
            });

            return services;
        }

        /// <summary>
        /// AddSwaggerService method: Add configurations for the Swagger service.
        /// </summary>
        /// <param name="services">The services</param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerService(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc(
                    "v1",
                    new OpenApiInfo { Title = "Maps API", Version = "v1" }
                );
                option.TagActionsBy(api => new[] { api.GroupName });
                option.DocInclusionPredicate((name, api) => true);
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                option.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
            });

            return services;
        }

        /// <summary>
        /// AddLocalizationService method: Add the corresponding localization to platform.
        /// </summary>
        /// <param name="services">The services</param>
        /// <returns></returns>
        public static IServiceCollection AddLocalizationService(this IServiceCollection services)
        {
            services.AddLocalization();

            var separator = new NumberFormatInfo()
            {
                NumberDecimalDigits = 0,
                NumberGroupSeparator = "."
            };
            var culture = new CultureInfo("en")
            {
                NumberFormat = separator
            };

            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            return services;
        }
    }
}
