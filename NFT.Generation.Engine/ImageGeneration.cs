﻿using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace NFT.Generation.Engine
{
    public class ImageGeneration : IImageGeneration
    {
        private readonly Random _random = new Random();

        public List<CompleteImageInfo> GenerateImages(CompiledAssets assets, int numOfGenerated, string saveDir)
        {
            var images = new List<CompleteImageInfo>();
            var current = 0;

            while(current < numOfGenerated)
            {
                var completedImage = new CompleteImageInfo()
                {
                    Name = $"BB Beast #{current}",
                    Path = Path.Combine(saveDir, $"{current}.png"),
                    AssetInfos = FetchAssetParts(assets)
                };
                images.Add(completedImage);
                CreateImage(completedImage);
                current++;
            }

            return images;
        }

        private List<AssetInfo> FetchAssetParts(CompiledAssets assets)
        {
            var result = new List<AssetInfo>();
            foreach(var asset in assets)
            {
                // foreach asset class, randomly pick
                var index = _random.Next(0, asset.Value.Count());
                var picked = asset.Value[index];
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
