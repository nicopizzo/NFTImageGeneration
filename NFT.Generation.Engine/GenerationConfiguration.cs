using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pinata.Client;

namespace NFT.Generation.Engine
{
    public static class GenerationConfiguration
    {
        public static IServiceCollection AddNFTGenerationServices(this IServiceCollection services, IConfiguration config)
        {
            var uploader = config.GetSection("Uploader").Value;
            switch (uploader)
            {
                case "Pinata":
                    var pinConfig = new Config()
                    {
                        ApiKey = config.GetSection("Pinata:ApiKey").Value,
                        ApiSecret = config.GetSection("Pinata:ApiSecret").Value
                    };
                    services.AddSingleton<IPinataClient, PinataClient>(f => new PinataClient(pinConfig));
                    services.AddSingleton<IImageUploader, PinataImageUploader>();
                    break;

                case "Infura":
                    var infuraConfig = new InfuraConfig()
                    {
                        ProjectId = config.GetSection("Infura:ProjectId").Value,
                        ProjectSecret = config.GetSection("Infura:ProjectSecret").Value,
                        Endpoint = config.GetSection("Infura:Endpoint").Value
                    };
                    services.AddSingleton(infuraConfig);
                    services.AddHttpClient<IImageUploader, InfuraImageUploader>(client =>
                    {
                        client.BaseAddress = new Uri(infuraConfig.Endpoint);
                    });
                    break;

                default: throw new ArgumentException(uploader);
            }

            services.AddSingleton<IAssetParser, AssetParser>();
            services.AddSingleton<IImageGeneration, ImageGeneration>();
            services.AddSingleton<IMetadataGeneration, MetadataGeneration>();
            
            return services;
        }
    }
}
