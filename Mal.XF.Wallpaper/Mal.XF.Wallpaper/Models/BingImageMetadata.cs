using System;
using System.Collections.Generic;
using Mal.XF.Infra.Extensions;

namespace Mal.XF.Wallpaper.Models
{
    internal class BingImageMetadata
    {
        public DateTime UpdateDate { get; set; }

        public IReadOnlyList<BingImage> Images { get; set; }

        public bool IsValid() => (DateTime.Now - this.UpdateDate).TotalHours < 11;
        public bool IsSmallValid() => (DateTime.Now - this.UpdateDate).TotalMinutes < 10;

        public static BingImageMetadata BuildFromBingImages(BingImages bingImages)=> 
            new BingImageMetadata
        {
            UpdateDate = DateTime.Now,
            Images = bingImages.Images.ToReadOnlyList()
        };
    }
}