using System;
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

        private async Task<IReadOnlyList<BingImage>> GetImagesFromCacheAsync()
        {
            var metadata = settingsService.GetMetadata();
            IReadOnlyList<BingImage> images;
            if (!metadata.IsValid())
                metadata = await this.GetMetadataAsync();

            await this.ClearImagesAsync(metadata.Images);
            return metadata.Images;
        }

        public async Task<BingImage> GetTodayImageAsync()
        {
            var images = await this.GetImagesAsync();
            return images[(int)RefreshImageType.ImageOfTheDay];
        }

        public Task SetImageAsScreenLockAsync(string imagePath)
            => this.wallpaperService.SetImageAsScreenLockAsync(imagePath);

        public Task SetImageAsWallpaperAsync(string imagePath)
            => this.wallpaperService.SetImageAsWallpaperAsync(imagePath);

        public async Task UpdateImagesIfNeededAsync()
        {
            var settings = settingsService.GetSettings();
            if (!settings.IsUpdateRequired)
                return;

            var images = await this.GetImagesFromCacheAsync();

            await Task.WhenAll(this.UpdateImageAsync(settings, images, RefreshImageType.ImageOfTheDay),
                               this.UpdateImageAsync(settings, images, RefreshImageType.ImageOfYesterday));
        }

        private async Task UpdateImageAsync(Settings settings, IReadOnlyList<BingImage> images, RefreshImageType type)
        {
            if (settings.RefreshWallpaper != type &&
                settings.RefreshScreenLock != type)
                return;

            var filePath = await this.DownloadImageAsync(images[(int)type]);

            if (settings.RefreshWallpaper == type)
                await this.SetImageAsWallpaperAsync(filePath);

            if (settings.RefreshScreenLock == type)
                await this.SetImageAsScreenLockAsync(filePath);
        }

        private async Task<BingImageMetadata> GetMetadataAsync()
        {
            var metadata = settingsService.GetMetadata();
            if (metadata.IsSmallValid())
                return metadata;

            var bingImages = await this.GetBingImagesAsync();
            metadata = BingImageMetadata.BuildFromBingImages(bingImages);
            await this.settingsService.SaveMetadataAsync(metadata);
            return metadata;
        }

        private async Task<BingImages> GetBingImagesAsync()
        {
            var webClient = new WebClient();

            var json = await webClient.DownloadStringAsync(HPImageArchivegUrl.Replace(numberOfImagesParamName, numberOfImages.ToString()));
            var images = JsonConvert.DeserializeObject<BingImages>(json);

            return images;
        }
        private async Task ClearImagesAsync(IReadOnlyCollection<BingImage> images)
        {
            var existingFiles = await this.fileService.GetFilesAsync(ImageDirectoryPath);
            var filesToRemove = existingFiles.Where(f => images.All(i => !f.EndsWith(downloadService.GetFileName(i))));

            foreach (var file in filesToRemove)
                await this.fileService.RemoveFileAsync(file);
        }
    }
}
