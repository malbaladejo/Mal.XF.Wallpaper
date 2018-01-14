using Mal.XF.Wallpaper.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Mal.XF.Infra.Extensions;
using System.Linq;
using Mal.XF.Infra.IO;
using Mal.XF.Infra.Net;

namespace Mal.XF.Wallpaper.Services
{
    internal class BingWallpaperService : IBingWallpaperService
    {
        private const string bingUrl = "https://www.bing.com";
        private const string numberOfImagesParamName = "{nbImage}";
        private static readonly string HPImageArchivegUrl = bingUrl + $"/HPImageArchive.aspx?format=js&idx=0&n={numberOfImagesParamName}";
        private const string ImageDirectoryPath = "imgDir";

        private readonly IDownloadService downloadService;
        private readonly IWallpaperService wallpaperService;
        private readonly IFileService fileService;

        public BingWallpaperService(IDownloadService downloadService,
            IWallpaperService wallpaperService,
            IFileService fileService)
        {
            this.downloadService = downloadService;
            this.wallpaperService = wallpaperService;
            this.fileService = fileService;
        }

        public async Task ClearImagesAsync(IReadOnlyCollection<BingImage> images)
        {
            var existingFiles = await this.fileService.GetFilesAsync(ImageDirectoryPath);
            var filesToRemove = existingFiles.Where(f => images.All(i => !f.EndsWith(this.downloadService.GetFileName(i))));

            foreach (var file in filesToRemove)
                await this.fileService.RemoveFileAsync(file);
        }

        public Task<string> DownloadImageAsync(BingImage image)
            => this.downloadService.DownloadImageAsync(image, ImageDirectoryPath);

        public async Task<IReadOnlyCollection<BingImage>> GetBinImagesAsync(int numberOfImages)
        {
            var webClient = new WebClient();
            var json = await webClient.DownloadStringTaskAsync(HPImageArchivegUrl.Replace(numberOfImagesParamName, numberOfImages.ToString()));
            var bingImages = JsonConvert.DeserializeObject<BingImages>(json);

            return bingImages.Images.ToReadOnlyCollection();
        }

        public async Task<BingImage> GetTodayBinImageAsync()
        {
            var bingImages = await this.GetBinImagesAsync(2);
            var bingImage = bingImages.First();
            return bingImage;
        }

        public Task SetImageAsScreenLockAsync(string imagePath)
            => this.wallpaperService.SetImageAsScreenLockAsync(imagePath);

        public Task SetImageAsWallpaperAsync(string imagePath)
            => this.wallpaperService.SetImageAsWallpaperAsync(imagePath);
    }
}
