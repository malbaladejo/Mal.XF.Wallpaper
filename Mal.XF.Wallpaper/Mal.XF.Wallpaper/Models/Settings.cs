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

        [JsonProperty("isWifiRequired")]
        public bool IsWifiRequired { get; set; } = true;

        [JsonProperty("lastUpdate")]
        public DateTime LastUpdate { get; set; } = DateTime.MinValue;


        [JsonIgnore]
        public bool IsUpdateRequired => this.RefreshWallpaper != RefreshImageType.None ||
                                        this.RefreshScreenLock != RefreshImageType.None;
    }
}
