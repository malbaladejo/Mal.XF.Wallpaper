using Mal.XF.Wallpaper.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mal.XF.Wallpaper.Services
{
    internal interface IBingWallpaperService
    {
        Task<BingImage> GetBinImageAsync();

        Task<string> DownloadImageAsync(BingImage image);

        void SetImageAsWallpaper(string imagePath);
    }
}
