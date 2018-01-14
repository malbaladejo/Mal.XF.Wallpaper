using Mal.XF.Infra;
using Mal.XF.Infra.Localisation;
using Mal.XF.Wallpaper.Localisation;
using Mal.XF.Wallpaper.Services;
using Mal.XF.Wallpaper.UWP.Services;
using Microsoft.Practices.Unity;
using System.Reflection;
using Mal.XF.Infra.UWP.PlatformInitializers;

namespace Mal.XF.Wallpaper.UWP
{
    internal class UwpInitializer : UwpInitializerBase
    {
        public override void RegisterTypes(IUnityContainer container)
        {
            base.RegisterTypes(container);

            container.RegisterType<IDownloadService, UwpDownloadService>();
            container.RegisterType<IWallpaperService, UwpWallpaperService>();
            container.Resolve<ITranslationManager>().Register(new LocalTranslationProvider("Mal.XF.Wallpaper.UWP.Localisation.Resources", typeof(LocalTranslationProvider).GetTypeInfo().Assembly));
        }
    }
}
