using Mal.XF.Infra.Android.IO;
using Mal.XF.Wallpaper.Services;

namespace Mal.XF.Wallpaper.Droid.Services
{
    internal static class BingWallpaperServiceFactory
    {
        public static IBingWallpaperService Create()
            => new BingWallpaperService(new AndroidDownloadService(new AndroidFileService()),
                new AndroidWallpaperService(), new AndroidFileService(), new SettingsService());
    }
}