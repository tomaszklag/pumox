using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Pumox.Infrastructure.Mvc
{
    public static class Extensions
    {
        public static IMvcCoreBuilder AddCustomMvc(this IServiceCollection services)
            => services
                .AddMvcCore()
                .AddFluentValidation(fv => {
                    fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                })
                .AddJsonFormatters()
                .AddDefaultJsonOptions()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

        private static IMvcCoreBuilder AddDefaultJsonOptions(this IMvcCoreBuilder builder)
            => builder.AddJsonOptions(o =>
            {
                o.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                o.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                o.SerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;
                o.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
                o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                o.SerializerSettings.Formatting = Formatting.Indented;
                o.SerializerSettings.Converters.Add(new StringEnumConverter());
            });

        public static void AddErrorHandlerMiddleware(this IServiceCollection services)
            => services.AddScoped<ErrorHandlerMiddleware>();  

        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
            => builder.UseMiddleware<ErrorHandlerMiddleware>();          
    }
}