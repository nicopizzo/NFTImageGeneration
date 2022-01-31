using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NFT.Generation.Engine
{
    public class RarityAnalizer : IRarityAnalizer
    {
        private readonly IAssetParser _AssetParser;
        private Dictionary<AssetPart, Dictionary<string, int>> _Results = new Dictionary<AssetPart, Dictionary<string, int>>();

        public RarityAnalizer(IAssetParser parser)
        {
            _AssetParser = parser;
        }

        public string AnalyzeRarities(string assetPath, string metaDataPath)
        {
            var assetInfo = _AssetParser.ParseAssets(assetPath);
            InitResults(assetInfo);

            var metaDataFiles = Directory.GetFiles(metaDataPath);
            var settings = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            foreach (var metaDataFile in metaDataFiles)
            {
                var rawData = File.ReadAllText(metaDataFile);
                var parsed = JsonSerializer.Deserialize<MetadataModel>(rawData, settings);
                AnalyizeAttributes(parsed);
            }

            return GenerateHtmlReport();
        }

        private void InitResults(CompiledAssets? assetInfo)
        {
            if (assetInfo == null) return;
            foreach(var asset in assetInfo)
            {
                var assetResult = new Dictionary<string, int>();
                foreach(var a in asset.Value.Assets)
                {
                    assetResult.Add(a.Name, 0);
                }
                _Results.Add(asset.Key, assetResult);
            }
        }

        private void AnalyizeAttributes(MetadataModel? metadata)
        {
            if(metadata == null) return;
            foreach(var attr in metadata.Attributes)
            {
                var type = Enum.Parse<AssetPart>(attr.Trait_Type);
                _Results[type][attr.Value]++;
            }
        }

        private string GenerateHtmlReport()
        {
            var htmlReport = new StringBuilder();

            htmlReport.AppendLine("<h1>Attribue Analysis</h1>");
            foreach(var attr in _Results)
            {
                htmlReport.AppendLine($"<h3>{attr.Key}</h3");
                foreach(var a in attr.Value)
                {
                    htmlReport.AppendLine($"<p>{a.Key}\t{a.Value}</p>");
                }
            }

            return htmlReport.ToString();
        }
    }
}
