using Newtonsoft.Json;

namespace Mal.XF.Wallpaper.Models
{
    internal class BingImage
    {
        public const int TodayImage = 0;
        public const int YesterdayImage = 1;

        [JsonProperty("bot")]
        public long Bot { get; set; }

        [JsonProperty("copyright")]
        public string Copyright { get; set; }

        [JsonProperty("copyrightlink")]
        public string Copyrightlink { get; set; }

        [JsonProperty("drk")]
        public long Drk { get; set; }

        [JsonProperty("enddate")]
        public string Enddate { get; set; }

        [JsonProperty("fullstartdate")]
        public string Fullstartdate { get; set; }

        [JsonProperty("hs")]
        public object[] Hs { get; set; }

        [JsonProperty("hsh")]
        public string Hsh { get; set; }

        [JsonProperty("quiz")]
        public string Quiz { get; set; }

        [JsonProperty("startdate")]
        public string Startdate { get; set; }

        [JsonProperty("top")]
        public long Top { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("urlbase")]
        public string Urlbase { get; set; }

        [JsonProperty("wp")]
        public bool Wp { get; set; }
    }
}