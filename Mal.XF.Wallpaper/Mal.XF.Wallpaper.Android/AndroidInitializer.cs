using Mal.XF.Infra.Android.PlatformInitializers;
using Mal.XF.Infra.Localisation;
using Mal.XF.Wallpaper.Droid.Services;
using Mal.XF.Wallpaper.Localisation;
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
            container.RegisterType<IBackgroundUpdateService, AndroidBackgroundUpdateService>();
            container.Resolve<ITranslationManager>().Register(new LocalTranslationProvider("Mal.XF.Wallpaper.Droid.Localisation.Resources", typeof(LocalTranslationProvider).Assembly));
        }
    }
}

