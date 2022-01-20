using Pinata.Client;
using Pinata.Client.Models;

namespace NFT.Generation.Engine
{
    public class PinataImageUploader : IImageUploader
    {
        private readonly IPinataClient _Client;

        public PinataImageUploader(IPinataClient pinataClient)
        {
            _Client = pinataClient;
        }

        public async Task UploadDirectory(string directoryPath)
        {
            var dir = new DirectoryInfo(directoryPath);
            var files = dir.GetFiles();
            await PerformDirectoryUpload(files, dir.Name);
        }

        public async Task<List<CompleteImageInfo>> UploadImages(List<CompleteImageInfo> images)
        {
            var files = images.Select(f => new FileInfo(f.Path)).ToArray();

            var response = await PerformDirectoryUpload(files, "bbbeast");

            var hash = response.IpfsHash;
            UpdateImageInfo(images, hash);
            return images;
        }

        private async Task<PinFileToIpfsResponse> PerformDirectoryUpload(FileInfo[] infos, string baseDirectory)
        {
            var response = await _Client.Pinning.PinFileToIpfsAsync(content =>
            {
                foreach (var info in infos)
                {
                    var bytes = File.ReadAllBytes(info.FullName);
                    content.AddPinataFile(new ByteArrayContent(bytes), $"{baseDirectory}/{info.Name}");
                }
            });
            return response;
        }

        private void UpdateImageInfo(List<CompleteImageInfo> images, string hash)
        {
            foreach(var image in images)
            {
                image.UploadedPath = $"ipfs://{hash}/{image.Index}.png";
            }
        }
    }
}
