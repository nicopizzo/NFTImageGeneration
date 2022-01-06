using NFT.Generation.Engine;
using NFT.Generation.Engine.Interfaces;

Console.WriteLine("Hello, World!");

IAssetParser parser = new AssetParser();
var assets = parser.ParseAssets(@"G:\Downloads\bb_beast");

IImageGeneration generation = new ImageGeneration();
var images = generation.GenerateImages(assets, 50, @"G:\Downloads\bb_generated");

IMetadataGeneration metadata = new MetadataGeneration();
metadata.GenerateMetadata(images, @"G:\Downloads\bb_generated_meta");

Console.ReadLine();
