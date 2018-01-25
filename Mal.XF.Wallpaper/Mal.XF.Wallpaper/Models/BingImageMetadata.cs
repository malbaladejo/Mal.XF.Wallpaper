using System;
using System.Collections.Generic;
using Mal.XF.Infra.Extensions;

namespace Mal.XF.Wallpaper.Models
{
    internal class BingImageMetadata
    {
        public DateTime UpdateDate { get; set; }

        public IReadOnlyList<BingImage> Images { get; set; }
    }
}