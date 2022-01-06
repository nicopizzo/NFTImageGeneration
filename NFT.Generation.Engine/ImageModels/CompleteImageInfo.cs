namespace NFT.Generation.Engine.ImageModels
{
    public class CompleteImageInfo
    {
        public string Name { get; set; }
        public int Index { get; set; }
        public string Path { get; set; }
        public string UploadedPath { get; set; }
        public List<AssetInfo> AssetInfos { get; set; }
    }
}
