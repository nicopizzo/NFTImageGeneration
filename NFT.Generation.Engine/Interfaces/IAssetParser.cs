namespace NFT.Generation.Engine.Interfaces
{
    public interface IAssetParser
    {
        CompiledAssets ParseAssets(string path);
    }
}
