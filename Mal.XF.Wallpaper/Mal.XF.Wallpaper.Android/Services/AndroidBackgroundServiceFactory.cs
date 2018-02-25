using Mal.XF.Infra.Android.IO;
using Mal.XF.Infra.Android.Net;
using Mal.XF.Infra.IO;
using Mal.XF.Infra.Log;
using Mal.XF.Infra.Net;
using Mal.XF.Wallpaper.Services;
using Mal.XF.Wallpaper.StateMachines;

namespace Mal.XF.Wallpaper.Droid.Services
{
    internal static class AndroidBackgroundServiceFactory
    {
        private static IWallpaperBackgroundService wallpaperBackgroundService;
        private static IBackgroundUpdateService backgroundUpdateService;
        private static IFileService fileService;
        private static ILocalStorageService localStorageService;
        private static ILogger logger;
        private static INetworkService networkService;
        private static IBingWallpaperService bingWallpaperService;
        private static AndroidDownloadService androidDownloadService;
        private static IBingWallpaperRepository bingWallpaperRepository;

        public static IWallpaperBackgroundService CreateWallpaperBackgroundUpdateService()
        {
            return wallpaperBackgroundService ?? (wallpaperBackgroundService = new WallpaperBackgroundService(
                                               CreateLocalStorageService(),
                                               CreateBingWallpaperService(),
                                               new AndroidWallpaperService()));
        }

        public static StateFactory CreateStateFactory()
        {
            return new StateFactory(CreateLocalStorageService(),
                CreateNetworkService(),
                CreateWallpaperBackgroundUpdateService(),
                CreateBingWallpaperRepository(),
                CreateBackgroundUpdateService(),
                CreateLogger()
                );
        }

        public static IBackgroundUpdateService CreateBackgroundUpdateService()
        {
            return backgroundUpdateService ?? (backgroundUpdateService = new AndroidBackgroundUpdateService(CreateLocalStorageService(), CreateLogger()));
        }

        private static IFileService CreateFileService()
        {
            return fileService ?? (fileService = new AndroidFileService());
        }

        private static ILocalStorageService CreateLocalStorageService()
        {
            return localStorageService ?? (localStorageService = new LocalStorageService());
        }

        public static ILogger CreateLogger()
        {
            return logger ?? (logger = new Logger(new LogManager()));
        }

        public static INetworkService CreateNetworkService()
        {
            return networkService ?? (networkService = new AndroidNetworkService());
        }

        public static IBingWallpaperService CreateBingWallpaperService()
        {
            return bingWallpaperService ?? (bingWallpaperService = new BingWallpaperService(CreateAndroidDownloadService(), CreateFileService()));
        }

        public static AndroidDownloadService CreateAndroidDownloadService()
        {
            return androidDownloadService ?? (androidDownloadService = new AndroidDownloadService(CreateFileService()));
        }

        public static IBingWallpaperRepository CreateBingWallpaperRepository()
        {
            return bingWallpaperRepository ?? (bingWallpaperRepository = new BingWallpaperRepository(CreateBingWallpaperService(), CreateLocalStorageService()));
        }
    }
}