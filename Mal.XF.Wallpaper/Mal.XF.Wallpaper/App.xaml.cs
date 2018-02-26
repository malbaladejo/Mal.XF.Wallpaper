using Mal.XF.Infra.Extensions;
using Mal.XF.Infra.Localisation;
using Mal.XF.Infra.Log;
using Mal.XF.Wallpaper.Pages.Configuration;
using Mal.XF.Wallpaper.Pages.Main;
using Mal.XF.Wallpaper.Services;
using Mal.XF.Wallpaper.StateMachines;
using Microsoft.Practices.Unity;
using Prism.Unity;
using System;

namespace Mal.XF.Wallpaper
{
    public partial class App 
    {
        private ILogger logger;

        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
        }

        protected override void OnInitialized()
        {
            this.InitializeComponent();
            base.OnInitialized();
            this.logger = this.Container.Resolve<ILogger>();

            this.StartBackgroundServiceIfNeeded();
        }

        private void StartBackgroundServiceIfNeeded()
        {
            try
            {
                this.logger.Info($"{nameof(App)}.{nameof(this.StartBackgroundServiceIfNeeded)}");
                var stateFactory = this.Container.Resolve<StateFactory>();
                var stateMachine = new StateMachine(stateFactory.GetInitialStateForDeviceBoot(), this.logger);
                stateMachine.Execute();
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
