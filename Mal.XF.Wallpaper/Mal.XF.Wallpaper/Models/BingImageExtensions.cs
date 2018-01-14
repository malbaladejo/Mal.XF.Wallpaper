using System.Text.RegularExpressions;
using System.Linq;

namespace Mal.XF.Wallpaper.Models
{
    internal static class BingImageExtensions
    {
        public static string GetFullUrl(this BingImage image)
        {
            return $"https://www.bing.com{image.Url}";
        }

        public static string GetFileName(this BingImage image)
        {
            var split = image.GetFullUrl().Split("/".ToCharArray());
            return split.Last();
        }

        public static string GetMobileFullUrl(this BingImage image)
        {
            var regex = new Regex("(.*_)([0-9]*x[0-9]*)(.jpg)");
            return regex.Replace(image.GetFullUrl(), "${1}1080x1920$3");
        }

        public static string GetMobileFileName(this BingImage image)
        {
            var split = image.GetMobileFullUrl().Split("/".ToCharArray());
            return split.Last();
        }
    }
}