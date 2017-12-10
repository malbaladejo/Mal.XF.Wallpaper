using Mal.XF.Wallpaper.Models;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;

namespace Mal.XF.Wallpaper.Services
{
    internal class BingWallpaperService : IBingWallpaperService
    {
        private const string bingUrl = "https://www.bing.com";
        private const string HPImageArchivegUrl = bingUrl + "/HPImageArchive.aspx?format=js&idx=0&n=1";


        private readonly IDownloadService downloadService;
        private readonly IWallpaperService wallpaperService;

        public BingWallpaperService(IDownloadService downloadService, IWallpaperService wallpaperService)
        {
            this.downloadService = downloadService;
            this.wallpaperService = wallpaperService;
        }

        public Task<string> DownloadImageAsync(BingImage image)
            => this.downloadService.DownloadImageAsync(image);

        public async Task<BingImage> GetBinImageAsync()
        {
            using (var webClient = new WebClient())
            {
                var json = await webClient.DownloadStringTaskAsync(HPImageArchivegUrl);
                var bingImages = JsonConvert.DeserializeObject<BingImages>(json);

                var bingImage = bingImages.Images[0];
                return bingImage;
            }
        }

        public void SetImageAsWallpaper(string imagePath)
            => this.wallpaperService.SetImageAsWallpaper(imagePath);
    }
}
