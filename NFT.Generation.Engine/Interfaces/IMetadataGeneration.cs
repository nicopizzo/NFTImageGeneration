namespace NFT.Generation.Engine.Interfaces
{
    public interface IMetadataGeneration
    {
        void GenerateMetadata(List<CompleteImageInfo> completeImages, string saveDir, string preMintUrl = "");
    }
}
