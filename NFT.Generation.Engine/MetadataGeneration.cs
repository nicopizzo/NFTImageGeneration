using System.Text.Json;

namespace NFT.Generation.Engine
{
    public class MetadataGeneration : IMetadataGeneration
    {
        public void GenerateMetadata(List<CompleteImageInfo> completeImages, string saveDir, string preMintUrl = "")
        {
            SetupDirectories(saveDir, preMintUrl);
            var serializerSettings = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            foreach (var image in completeImages)
            {
                var metadata = new MetadataModel()
                {
                    Name = image.Name,
                    Image = image.UploadedPath,
                    Attributes = image.AssetInfos.Select(f => new AttributeMetadataModel()
                    {
                        Trait_Type = f.Part.ToString(),
                        Value = f.Name
                    })
                };
                var data = JsonSerializer.Serialize(metadata, serializerSettings);
                File.WriteAllText(Path.Combine(saveDir, "minted", image.Index.ToString() + ".json"), data);

                if (!string.IsNullOrEmpty(preMintUrl))
                {
                    metadata.Image = preMintUrl;
                    metadata.Attributes = null;
                    data = JsonSerializer.Serialize(metadata, serializerSettings);
                    File.WriteAllText(Path.Combine(saveDir, "preminted", image.Index.ToString() + ".json"), data);
                }
            }
        }

        private void SetupDirectories(string saveDir, string preMintUrl)
        {
            var path = Path.Combine(saveDir, "minted");
            if(!Directory.Exists(path)) Directory.CreateDirectory(path);

            if (!string.IsNullOrEmpty(preMintUrl))
            {
                path = Path.Combine(saveDir, "preminted");
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            }
        }
    }
}
