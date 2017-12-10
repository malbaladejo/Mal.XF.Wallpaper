using Newtonsoft.Json;

namespace Mal.XF.Wallpaper.Models
{
    internal class BingImages
    {
        [JsonProperty("images")]
        public BingImage[] Images { get; set; }

        [JsonProperty("tooltips")]
        public Tooltips Tooltips { get; set; }
    }
}
