namespace NFT.Generation.Engine.Interfaces
{
    public interface IImageUploader
    {
        Task<List<CompleteImageInfo>> UploadImages(List<CompleteImageInfo> images);
        Task UploadDirectory(string directory);
    }
}
