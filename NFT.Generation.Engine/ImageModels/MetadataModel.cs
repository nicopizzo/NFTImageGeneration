namespace NFT.Generation.Engine.ImageModels
{
    public class MetadataModel
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public IEnumerable<AttributeMetadataModel> Attributes { get; set; }
    }

    public class AttributeMetadataModel
    {
        public string Trait_Type { get; set; }
        public string Value { get; set; }
    }
}
