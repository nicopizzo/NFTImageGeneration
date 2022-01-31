using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace NFT.Generation.Engine
{
    public class ImageGeneration : IImageGeneration
    {
        public List<CompleteImageInfo> GenerateImages(CompiledAssets assets, int numOfGenerated, string saveDir, bool includeBackground = true)
        {
            var images = new List<CompleteImageInfo>();
            var current = 0;

            while(current < numOfGenerated)
            {
                var completedImage = new CompleteImageInfo()
                {
                    Name = $"BB Beast #{current}",
                    Index = current,
                    Path = Path.Combine(saveDir, $"{current}.png"),
                    AssetInfos = FetchAssetParts(assets, includeBackground)
                };
                images.Add(completedImage);
                CreateImage(completedImage);
                current++;
            }

            return images;
        }

        private List<AssetInfo> FetchAssetParts(CompiledAssets assets, bool includeBackground)
        {
            var result = new List<AssetInfo>();
            foreach(var asset in assets)
            {
                if (!includeBackground && asset.Key == AssetPart.Background) continue;
                // foreach asset class, randomly pick by rarity
                var assetNameToGet = asset.Value.RarityTable.GetNextAsset();
                var picked = asset.Value.Assets.First(f => f.Name == assetNameToGet);
                result.Add(picked);
            }
            return result;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        private void CreateImage(CompleteImageInfo imageInfo)
        {
            Bitmap? finalImage = null;
            Graphics? graphics = null;
            foreach(var asset in imageInfo.AssetInfos.OrderByDescending(f => (int)f.Part))
            {
                var currentLayer = (Bitmap)Image.FromFile(asset.Path);
                // create the final base
                if (finalImage == null)
                {
                    finalImage = new Bitmap(currentLayer.Width, currentLayer.Height, PixelFormat.Format32bppArgb);
                    graphics = Graphics.FromImage(finalImage);
                    graphics.CompositingMode = CompositingMode.SourceOver;
                }

                graphics.DrawImage(currentLayer, 0, 0);  
            }

            finalImage.Save(imageInfo.Path);
        }
    }
}
