using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFT.Generation.Engine
{
    internal class InfuraImageUploader : IImageUploader
    {
        private readonly HttpClient _Client;
        private readonly InfuraConfig _Config;

        public InfuraImageUploader(HttpClient client, InfuraConfig config)
        {
            _Client = client;
            _Config = config;
        }

        public async Task UploadDirectory(string directory)
        {
            //_Client.PostAsync("/api/v0/add", )
        }

        public Task<List<CompleteImageInfo>> UploadImages(List<CompleteImageInfo> images)
        {
            throw new NotImplementedException();
        }

        private string GenerateAuth()
        {
            var s = $"{_Config.ProjectId}:{_Config.ProjectSecret}";
            var plainTextBytes = Encoding.UTF8.GetBytes(s);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}
