using System.Text.Json;

namespace NFT.Generation.Engine
{
    public class AssetParser : IAssetParser
    {
        public CompiledAssets ParseAssets(string path)
        {
            var assetDict = new CompiledAssets();
            DirectoryInfo directory = new DirectoryInfo(path);
            var partDirs = directory.GetDirectories();
            foreach (var partDir in partDirs)
            {              
                var rarityInfo = GetRarity(partDir);
                var assetList = new List<AssetInfo>();
                var dirResult = ProcessDirectory(partDir);

                foreach (var file in dirResult.Item2)
                {
                    var assetInfo = ProcessFile(file, dirResult.Item1, rarityInfo);
                    assetList.Add(assetInfo);
                }

                var assetPart = new CompiledAssetPart()
                {
                    Assets = assetList,
                    RarityTable = new RarityTable(rarityInfo)
                };

                assetDict.Add(dirResult.Item1, assetPart);
            }

            return assetDict;
        }

        public void GenerateRarityFiles(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            var partDirs = directory.GetDirectories();
            foreach (var partDir in partDirs)
            {
                var rarities = new List<RarityInfo>();
                
                var files = partDir.GetFiles("*.png");
                double rarity = 1 / (double)files.Length;
                foreach (var file in files)
                {
                    rarities.Add(new RarityInfo()
                    {
                        AssetName = file.Name,
                        Rarity = rarity
                    });
                }
                var rawJson = JsonSerializer.Serialize(rarities, new JsonSerializerOptions() { WriteIndented = true });
                File.WriteAllText(Path.Combine(partDir.FullName, "rarity.json"), rawJson);
            }
        }

        private List<RarityInfo> GetRarity(DirectoryInfo info)
        {
            var path = Path.Combine(info.FullName, "rarity.json");
            var data = File.ReadAllText(path);
            var rarities = JsonSerializer.Deserialize<List<RarityInfo>>(data);
            return rarities;
        }

        private AssetInfo ProcessFile(FileInfo file, AssetPart part, List<RarityInfo>? rarities)
        {
            var rarity = rarities?.FirstOrDefault(f => f.AssetName == file.Name);
            var assetInfo = new AssetInfo()
            {
                Name = file.Name,
                Part = part,
                Path = file.FullName
            };
            return assetInfo;
        }

        private (AssetPart, FileInfo[]) ProcessDirectory(DirectoryInfo partDir)
        {
            var split = partDir.Name.Split('_');
            var part = Enum.Parse<AssetPart>(split[1]);
            var files = partDir.GetFiles("*.png");

            return (part, files);
        }
    }
}
