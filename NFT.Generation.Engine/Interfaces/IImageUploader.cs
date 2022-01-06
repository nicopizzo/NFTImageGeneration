namespace NFT.Generation.Engine.Interfaces
{
    public interface IImageUploader
    {
        public List<CompleteImageInfo> UploadImages(List<CompleteImageInfo> images);
    }
}
