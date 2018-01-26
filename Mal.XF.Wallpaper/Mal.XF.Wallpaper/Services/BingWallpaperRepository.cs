using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Mal.XF.Wallpaper.Models;

namespace Mal.XF.Wallpaper.Services
{
    internal class BingWallpaperRepository: IBingWallpaperRepository
    {
        private readonly IBingWallpaperService bingWallpaperService;
        private readonly ILocalStorageService localStorageService;

        public BingWallpaperRepository(IBingWallpaperService bingWallpaperService, ILocalStorageService localStorageService)
        {
            this.bingWallpaperService = bingWallpaperService;
            this.localStorageService = localStorageService;
        }

        public async Task<IReadOnlyList<BingImage>> GetImagesAsync()
        {
            var metadata = this.localStorageService.GetMetadata();
            if (IsMetaUpToDate(metadata))
                return metadata.Images;

            var images = await this.bingWallpaperService.GetImagesAsync();
            await SaveMetadataAsync(images);

            return images;
        }

        private async Task SaveMetadataAsync(IReadOnlyList<BingImage> images)
        {
            await this.localStorageService.SaveMetadataAsync(new BingImageMetadata
            {
                UpdateDate = DateTime.Now,
                Images = images
            });
        }

        private static bool IsMetaUpToDate(BingImageMetadata metadata) => (DateTime.Now - metadata.UpdateDate).TotalMinutes < 10;

        public Task<string> DownloadImageAsync(BingImage image) => this.bingWallpaperService.DownloadImageAsync(image);
    }
}
