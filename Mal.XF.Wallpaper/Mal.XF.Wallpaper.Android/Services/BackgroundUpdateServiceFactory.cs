using Mal.XF.Wallpaper.Services;

namespace Mal.XF.Wallpaper.Droid.Services
{
    internal static class BackgroundUpdateServiceFactory
    {
        public static IBackgroundUpdateService Create()
            => new AndroidBackgroundUpdateService(new SettingsService());
    }
}