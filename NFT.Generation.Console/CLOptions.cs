using CommandLine;

namespace NFT.Generation.Console
{
    internal class CLOptions
    {
        [Option('g', "generaterarities")]
        public bool GenerateRarityFiles { get; set; }
    }
}
