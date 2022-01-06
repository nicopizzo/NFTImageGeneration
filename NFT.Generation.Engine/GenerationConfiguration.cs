using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pinata.Client;

namespace NFT.Generation.Engine
{
    public static class GenerationConfiguration
    {
        public static IServiceCollection AddNFTGenerationServices(this IServiceCollection services, IConfiguration config)
        {
            var pinConfig = new Config()
            {
                ApiKey = config.GetSection("Pinata:ApiKey").Value,
                ApiSecret = config.GetSection("Pinata:ApiSecret").Value
            };
            services.AddSingleton<IPinataClient, PinataClient>(f => new PinataClient(pinConfig));

            services.AddSingleton<IAssetParser, AssetParser>();
            services.AddSingleton<IImageGeneration, ImageGeneration>();
            services.AddSingleton<IMetadataGeneration, MetadataGeneration>();
            services.AddSingleton<IImageUploader, PinataImageUploader>();

            return services;
        }
    }
}
