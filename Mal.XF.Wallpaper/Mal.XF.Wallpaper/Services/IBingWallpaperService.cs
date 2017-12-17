using Mal.XF.Wallpaper.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mal.XF.Wallpaper.Services
{
    internal interface IBingWallpaperService
    {
        Task<BingImage> GetTodayBinImageAsync();
        Task<IReadOnlyCollection<BingImage>> GetBinImagesAsync(int numberOfImages);
        Task ClearImagesAsync(IReadOnlyCollection<BingImage> images);

        Task<string> DownloadImageAsync(BingImage image);

        Task SetImageAsWallpaperAsync(string imagePath);
        Task SetImageAsScreenLockAsync(string imagePath);
    }
}
