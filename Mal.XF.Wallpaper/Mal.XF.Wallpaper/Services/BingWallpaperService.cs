using System;
using Mal.XF.Wallpaper.Models;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using Mal.XF.Infra.Extensions;
using System.Linq;
using Mal.XF.Infra.IO;

namespace Mal.XF.Wallpaper.Services
{
    internal class BingWallpaperService : IBingWallpaperService
    {
        private const string bingUrl = "https://www.bing.com";
        private const string numberOfImagesParamName = "{nbImage}";
        private static readonly string HPImageArchivegUrl = bingUrl + $"/HPImageArchive.aspx?format=js&idx=0&n={numberOfImagesParamName}";
        private const string ImageDirectoryPath = "imgDir";
        private const int numberOfImages = 2;

        private readonly IDownloadService downloadService;
        private readonly IWallpaperService wallpaperService;
        private readonly IFileService fileService;
        private readonly ISettingsService settingsService;

        public BingWallpaperService(IDownloadService downloadService,
                                    IWallpaperService wallpaperService,
                                    IFileService fileService,
                                    ISettingsService settingsService)
        {
            this.downloadService = downloadService;
            this.wallpaperService = wallpaperService;
            this.fileService = fileService;
            this.settingsService = settingsService;
        }

        public Task<string> DownloadImageAsync(BingImage image)
            => this.downloadService.DownloadImageAsync(image, ImageDirectoryPath);

        public async Task<IReadOnlyList<BingImage>> GetImagesAsync()
        {
            var metadata = await this.GetMetadataAsync();
            await this.ClearImagesAsync(metadata.Images);
            return metadata.Images;
        }

        public async Task<BingImage> GetTodayImageAsync()
        {
            var images = await this.GetImagesAsync();
            return images[BingImage.TodayImage];
        }

        public Task SetImageAsScreenLockAsync(string imagePath)
            => this.wallpaperService.SetImageAsScreenLockAsync(imagePath);

        public Task SetImageAsWallpaperAsync(string imagePath)
            => this.wallpaperService.SetImageAsWallpaperAsync(imagePath);

        private async Task<BingImageMetadata> GetMetadataInCacheAsync()
        {
            var metadata = this.settingsService.GetMetadata();
            if (metadata != null)
            {
                if (metadata.IsValid())
                    return metadata;
            }

            return await this.GetMetadataAsync();
        }

        private async Task<BingImageMetadata> GetMetadataAsync()
        {
            var bingImages = await this.GetBingImagesAsync();
           return BingImageMetadata.BuildFromBingImages(bingImages);
        }

        private async Task<BingImages> GetBingImagesAsync()
        {
            using (var webClient = new WebClient())
            {
                var json = await webClient.DownloadStringTaskAsync(HPImageArchivegUrl.Replace(numberOfImagesParamName, numberOfImages.ToString()));
                var  images = JsonConvert.DeserializeObject<BingImages>(json);

                return images;
            }
        }
        private async Task ClearImagesAsync(IReadOnlyCollection<BingImage> images)
        {
            var existingFiles = await this.fileService.GetFilesAsync(ImageDirectoryPath);
            var filesToRemove = existingFiles.Where(f => images.All(i => !f.EndsWith(i.GetFileName())));

            foreach (var file in filesToRemove)
                await this.fileService.RemoveFileAsync(file);
        }
    }
}
