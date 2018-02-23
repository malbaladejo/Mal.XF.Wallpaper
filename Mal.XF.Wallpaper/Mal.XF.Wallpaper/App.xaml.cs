using System;
using Mal.XF.Infra;
using Mal.XF.Infra.Extensions;
using Mal.XF.Infra.Localisation;
using Mal.XF.Infra.Log;
using Mal.XF.Infra.Pages.Master;
using Mal.XF.Infra.Pages.MasterMenu;
using Mal.XF.Wallpaper.Pages.Configuration;
using Mal.XF.Wallpaper.Pages.Main;
using Mal.XF.Wallpaper.Services;
using Microsoft.Practices.Unity;
using Prism.Unity;
using Xamarin.Forms;
using Mal.XF.Infra.Navigation;
using Mal.XF.Wallpaper.Droid.Services;

namespace Mal.XF.Wallpaper
{
    public partial class App 
    {
        public NavigationPage NavigationPage { get; private set; }
        private ILogger logger;
        private IBackgroundUpdateService backgroundUpdateService;

        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
        }

        protected override void OnInitialized()
        {
            this.InitializeComponent();
            base.OnInitialized();
            this.logger = this.Container.Resolve<ILogger>();
            this.backgroundUpdateService = this.Container.Resolve<IBackgroundUpdateService>();

            try
            {
                this.logger.Info($"{nameof(App)}.{nameof(this.OnInitialized)}");
                this.backgroundUpdateService.StartIfNeeded();
            }
            catch (Exception e)
            {
                this.logger.Error($"{nameof(App)}.{nameof(this.OnInitialized)}", e);
            }
        }

        protected override void RegisterTypes()
        {
            base.RegisterTypes();

            this.RegisterViews();
            this.RegisterServices();
        }

        private void RegisterViews()
        {
            this.Container.RegisterViewForMasterDetailNavigation<MainPage, MainViewModel>(new MainDisplayableToken());
            this.Container.RegisterViewForMasterDetailNavigation<ConfigurationPage, ConfigurationViewModel>(new ConfigurationDisplayableToken());
        }

        private void RegisterServices()
        {
            this.Container.RegisterType<IBingWallpaperService, BingWallpaperService>();
            this.Container.RegisterType<ILocalStorageService, LocalStorageService>();
            this.Container.RegisterType<IWallpaperBackgroundService, WallpaperBackgroundService>();
            this.Container.RegisterType<IBingWallpaperRepository, BingWallpaperRepository>();
        }

        internal void RegisterTranslationProvider(ITranslationProvider provider)
        {
            this.Container.Resolve<ITranslationManager>().Register(provider);
        }
    }
}
