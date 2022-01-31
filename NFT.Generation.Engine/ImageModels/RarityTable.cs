namespace NFT.Generation.Engine.ImageModels
{
    public class RarityTable
    {
        private Random _Random = new Random();
        private List<Tuple<double, double, string>> _Table = new List<Tuple<double, double, string>>();

        public RarityTable(List<RarityInfo> infos)
        {
            InitTable(infos);
        }

        public string GetNextAsset()
        {
            var v = _Random.NextDouble();
            var rarity = _Table.First(f => v <= f.Item2 && v >= f.Item1);
            return rarity.Item3;
        }

        private void InitTable(List<RarityInfo> infos)
        {
            double total = 0;
            int totalCount = infos.Count;
            for (var i = 0; i < totalCount; i++)
            {
                var rarity = infos[i];
                var upperBound = total + rarity.Rarity;
                if ((i + 1) == totalCount) upperBound = 1;
                _Table.Add(new Tuple<double, double, string>(total, upperBound, rarity.AssetName));
                total = upperBound;
            }
        }
    }
}
