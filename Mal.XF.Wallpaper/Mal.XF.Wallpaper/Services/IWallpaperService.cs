using System.Threading.Tasks;

namespace Mal.XF.Wallpaper.Services
{
    internal interface IWallpaperService
    {
        Task SetImageAsWallpaperAsync(string imagePath);
        Task SetImageAsScreenLockAsync(string imagePath);
    }
}
