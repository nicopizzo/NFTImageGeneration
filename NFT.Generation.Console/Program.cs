using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NFT.Generation.Engine;
using NFT.Generation.Engine.Interfaces;

var config = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

var serviceProvider = new ServiceCollection()
    .AddNFTGenerationServices(config)
    .BuildServiceProvider();

Console.WriteLine("Parsing assets");
IAssetParser parser = serviceProvider.GetRequiredService<IAssetParser>();
var assets = parser.ParseAssets(@"G:\Downloads\bb_beast");

Console.WriteLine("Generating images");
IImageGeneration generation = serviceProvider.GetRequiredService<IImageGeneration>();
var images = generation.GenerateImages(assets, 50, @"G:\Downloads\bb_generated");

Console.WriteLine("Uploading images");
IImageUploader uploader = serviceProvider.GetRequiredService<IImageUploader>();
images = await uploader.UploadImages(images);

Console.WriteLine("Generating metadata");
IMetadataGeneration metadata = serviceProvider.GetRequiredService<IMetadataGeneration>();
metadata.GenerateMetadata(images, @"G:\Downloads\bb_generated_meta", $"ipfs://QmYr2SvfDbNt7pjRRvnAUC9qYDTZAsGaBC1scgyU9B4rgR");

await uploader.UploadDirectory(@"G:\Downloads\bb_generated_meta\minted");
await uploader.UploadDirectory(@"G:\Downloads\bb_generated_meta\preminted");


Console.ReadLine();
