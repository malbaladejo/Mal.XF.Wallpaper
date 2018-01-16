using System;
using System.Collections.Generic;

namespace Mal.XF.Wallpaper.Models
{
    internal class BingImageMetadata
    {
        public DateTime UpdateDate { get; set; }

        public IReadOnlyCollection<BingImage> Images { get; set; }

        public bool IsValid() => (DateTime.Now - this.UpdateDate).TotalHours < 11;
    }
}