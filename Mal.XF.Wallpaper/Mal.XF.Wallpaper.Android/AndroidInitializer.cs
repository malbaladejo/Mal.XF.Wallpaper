using Mal.XF.Infra.Android.PlatformInitializers;
using Mal.XF.Wallpaper.Droid.Services;
using Mal.XF.Wallpaper.Services;
using Microsoft.Practices.Unity;

namespace Mal.XF.Wallpaper.Droid
{
    internal class AndroidInitializer : AndroidInitializerBase
    {
        public override void RegisterTypes(IUnityContainer container)
        {
            base.RegisterTypes(container);

            container.RegisterType<IDownloadService, AndroidDownloadService>();
            container.RegisterType<IWallpaperService, AndroidWallpaperService>();
        }
    }
}

