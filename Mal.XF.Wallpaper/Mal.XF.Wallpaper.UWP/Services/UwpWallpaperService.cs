using Mal.XF.Wallpaper.Services;
using System.Threading.Tasks;

namespace Mal.XF.Wallpaper.UWP.Services
{
    internal class UwpWallpaperService : IWallpaperService
    {
        public async Task SetImageAsScreenLockAsync(string imagePath)
        {
            await Task.Delay(0);
        }

        public async Task SetImageAsWallpaperAsync(string imagePath)
        {
            await Task.Delay(0);
        }
    }
}