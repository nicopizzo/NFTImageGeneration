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
                var assetList = new List<AssetInfo>();
                var dirResult = ProcessDirectory(partDir);

                foreach (var file in dirResult.Item2)
                {
                    var assetInfo = ProcessFile(file, dirResult.Item1);
                    assetList.Add(assetInfo);
                }

                assetDict.Add(dirResult.Item1, assetList);
            }

            return assetDict;
        }

        private AssetInfo ProcessFile(FileInfo file, AssetPart part)
        {
            var assetInfo = new AssetInfo()
            {
                Name = file.Name,
                Part = part,
                Path = file.FullName
                // TODO Rarity
            };
            return assetInfo;
        }

        private (AssetPart, FileInfo[]) ProcessDirectory(DirectoryInfo partDir)
        {
            var split = partDir.Name.Split('_');
            var part = Enum.Parse<AssetPart>(split[1]);
            var files = partDir.GetFiles();

            return (part, files);
        }
    }
}
