using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
namespace JsonLocalizer;
public static class JsonLocalizerExtension
{
    public static IServiceCollection AddJsonLocalizer(this IServiceCollection services, string WebRootPath, params CultureInfo[] cultureInfos)
    {
        string AcceptLanguage = string.Empty;
        services.Configure<RequestLocalizationOptions>(options =>
        {
            options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
            options.SupportedCultures = cultureInfos;
            options.SupportedUICultures = cultureInfos;
            options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(async context =>
            {
                var userLangs = context.Request.Headers["Accept-Language"].ToString();
                var firstLang = userLangs.Split(',').FirstOrDefault();
                AcceptLanguage = string.IsNullOrEmpty(firstLang) ? "en-US" : firstLang;
                return await Task.FromResult(new ProviderCultureResult(AcceptLanguage, AcceptLanguage));
            }));
        });
        services.AddScoped(jsonLocalizer =>
        {
            return new JsonLocalizerManager(WebRootPath, AcceptLanguage);
        });
        return services;
    }
}
