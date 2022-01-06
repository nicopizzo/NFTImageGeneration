namespace NFT.Generation.Engine.Interfaces
{
    public interface IMetadataGeneration
    {
        public void GenerateMetadata(List<CompleteImageInfo> completeImages, string saveDir, string preMintUrl = "");
    }
}
