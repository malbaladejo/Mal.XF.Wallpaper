using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Mal.XF.Wallpaper.Models
{
    internal class Settings
    {
        [JsonProperty("refreshWallpaper")]
        public RefreshImageType RefreshWallpaper { get; set; }

        [JsonProperty("refreshScreenLock")]
        public RefreshImageType RefreshScreenLock { get; set; }
    }
}
