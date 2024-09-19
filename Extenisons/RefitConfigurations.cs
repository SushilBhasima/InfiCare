using InfiCare.Common.Interface;
using Newtonsoft.Json;
using Refit;

namespace InfiCare.Extenisons;

public static class RefitConfigurations
{
    public static IServiceCollection RefitConfigure(this IServiceCollection services)
    {
        // -- Default NewtonSoftJson Settings
        JsonConvert.DefaultSettings = () => new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(),
            MaxDepth = 10,
            CheckAdditionalContent = true,
        };
        RefitSettings defaultRefitSettings = new()
        {
            ContentSerializer = new NewtonsoftJsonContentSerializer()
        };
        services.AddRefitClient<IExchangeRateAPI>(defaultRefitSettings).ConfigureHttpClient(c => c.BaseAddress = new Uri("https://www.nrb.org.np"));

        return services;

    }
}
