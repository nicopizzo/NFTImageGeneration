namespace NFT.Generation.Engine.ImageModels
{
    public class AssetInfo
    {
        public string Name { get; set; }
        public AssetPart Part { get; set; }
        public string Path { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}