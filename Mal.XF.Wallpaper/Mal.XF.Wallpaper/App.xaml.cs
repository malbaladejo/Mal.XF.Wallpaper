using Mal.XF.Infra;
using Mal.XF.Infra.Extensions;
using Mal.XF.Infra.Localisation;
using Mal.XF.Wallpaper.Pages.Configuration;
using Mal.XF.Wallpaper.Pages.Main;
using Mal.XF.Wallpaper.Services;
using Microsoft.Practices.Unity;
using Prism.Unity;
using Xamarin.Forms;

namespace Mal.XF.Wallpaper
{
    public partial class App : ApplicationBase
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            this.InitializeComponent();
            this.NavigationService.NavigateAsync($"NavigationPage/{typeof(MainToken).FullName}");
        }

        protected override void RegisterTypes()
        {
            base.RegisterTypes();

            this.RegisterViews();
            this.RegisterServices();
        }

        private void RegisterViews()
        {
            this.Container.RegisterTypeForNavigation<NavigationPage>();

            this.Container.RegisterViewForNavigation<MainPage, MainViewModel, MainToken>();
            this.Container.RegisterViewForNavigation<ConfigurationPage, ConfigurationViewModel, ConfigurationToken>();
            //this.Container.RegisterViewForNavigation<AboutPage, AboutViewModel, AboutToken>();
        }

        private void RegisterServices()
        {
            this.Container.RegisterType<IBingWallpaperService, BingWallpaperService>();
            this.Container.RegisterType<ISettingsService, SettingsService>();
        }

        internal void RegisterTranslationProvider(ITranslationProvider provider)
        {
            this.Container.Resolve<ITranslationManager>().Register(provider);
        }
    }
}
