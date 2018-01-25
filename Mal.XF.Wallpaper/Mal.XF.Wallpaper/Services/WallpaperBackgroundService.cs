using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Mal.XF.Wallpaper.Models;

namespace Mal.XF.Wallpaper.Services
{
   internal class WallpaperBackgroundService: IWallpaperBackgroundService
    {
        private readonly ILocalStorageService localStorageService;
        private readonly IBingWallpaperService bingWallpaperService;
        private readonly IWallpaperService wallpaperService;

        public WallpaperBackgroundService(ILocalStorageService localStorageService, IBingWallpaperService bingWallpaperService, IWallpaperService wallpaperService)
        {
            this.localStorageService = localStorageService;
            this.bingWallpaperService = bingWallpaperService;
            this.wallpaperService = wallpaperService;
        }

        public async Task UpdateImagesAsync()
        {
            var settings = this.localStorageService.GetSettings();
            if (!settings.IsUpdateRequired)
                return;

            var images = await this.bingWallpaperService.GetImagesAsync();

            await Task.WhenAll(this.UpdateImageAsync(settings, images, RefreshImageType.ImageOfTheDay),
                this.UpdateImageAsync(settings, images, RefreshImageType.ImageOfYesterday));
        }

        private async Task UpdateImageAsync(Settings settings, IReadOnlyList<BingImage> images, RefreshImageType type)
        {
            if (settings.RefreshWallpaper != type &&
                settings.RefreshScreenLock != type)
                return;

            var filePath = await this.bingWallpaperService.DownloadImageAsync(images[(int)type]);

            if (settings.RefreshWallpaper == type)
                await this.wallpaperService.SetImageAsWallpaperAsync(filePath);

            if (settings.RefreshScreenLock == type)
                await this.wallpaperService.SetImageAsScreenLockAsync(filePath);
        }
    }
}
