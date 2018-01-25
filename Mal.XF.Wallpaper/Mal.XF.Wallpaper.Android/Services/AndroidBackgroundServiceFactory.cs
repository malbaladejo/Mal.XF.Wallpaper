using Mal.XF.Infra.Android.IO;
using Mal.XF.Infra.IO;
using Mal.XF.Wallpaper.Services;

namespace Mal.XF.Wallpaper.Droid.Services
{
    internal static class AndroidBackgroundServiceFactory
    {
        private static IWallpaperBackgroundService wallpaperBackgroundService;
        private static IBackgroundUpdateService backgroundUpdateService;
        private static IFileService fileService;
        private static ILocalStorageService localStorageService;

        public static IWallpaperBackgroundService CreateIBackgroundUpdateService2()
        {
            return wallpaperBackgroundService ?? (wallpaperBackgroundService = new WallpaperBackgroundService(
                                               CreateLocalStorageService(),
                                               new BingWallpaperService(new AndroidDownloadService(CreateFileService()), CreateFileService()),
                                               new AndroidWallpaperService()));
        }

        public static IBackgroundUpdateService CreateIBackgroundUpdateService()
        {
            return backgroundUpdateService ?? (backgroundUpdateService = new AndroidBackgroundUpdateService(CreateLocalStorageService()));
        }

        private static IFileService CreateFileService()
        {
            return fileService ?? (fileService = new AndroidFileService());
        }
        private static ILocalStorageService CreateLocalStorageService()
        {
            return localStorageService ?? (localStorageService = new LocalStorageService());
        }
    }
}