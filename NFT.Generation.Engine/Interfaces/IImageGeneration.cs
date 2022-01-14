namespace NFT.Generation.Engine.Interfaces
{
    public interface IImageGeneration
    {
        List<CompleteImageInfo> GenerateImages(CompiledAssets assets, int numOfGenerated, string saveDir, bool includeBackground = true);
    }
}
