namespace NFT.Generation.Engine.ImageModels
{
    public class CompiledAssets : Dictionary<AssetPart, CompiledAssetPart>
    {
    }

    public class CompiledAssetPart
    {
        public List<AssetInfo> Assets { get; set; }
        public RarityTable RarityTable { get;set; }
    }
}
