using CommandLine;

namespace NFT.Generation.Console
{
    internal class CLOptions
    {
        [Option('g', "generaterarities")]
        public bool GenerateRarityFiles { get; set; }

        [Option('a', "analyizerarities")]
        public bool AnalyizeRarity { get; set; }
    }
}
