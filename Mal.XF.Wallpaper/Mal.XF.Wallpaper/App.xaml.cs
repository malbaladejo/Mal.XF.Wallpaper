﻿using Mal.XF.Infra;
using Mal.XF.Infra.Extensions;
using Mal.XF.Infra.Localisation;
using Mal.XF.Infra.Pages.Master;
using Mal.XF.Infra.Pages.MasterMenu;
using Mal.XF.Wallpaper.Pages.Configuration;
using Mal.XF.Wallpaper.Pages.Main;
using Mal.XF.Wallpaper.Services;
using Microsoft.Practices.Unity;
using Prism.Unity;
using Xamarin.Forms;
using Mal.XF.Infra.Navigation;

namespace Mal.XF.Wallpaper
{
    public partial class App : ApplicationBase
    {
        public NavigationPage NavigationPage { get; private set; }

        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
        }

        protected override void OnInitialized()
        {
            this.InitializeComponent();

            // TODO a revoir
            var menu = new MasterMenuPage();
            var menuVm = new MasterMenuViewModel(this.NavigationService, this.Container.Resolve<IMasterDetailNavigationService>());
            menu.BindingContext = menuVm;
            var rootPage = new MasterPage(menu);
            // TODO a revoir

            this.MainPage = rootPage;

            menuVm.NavigateToFirst();
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
            this.Container.RegisterViewForMasterDetailNavigation<ConfigurationPage, ConfigurationDisplayableToken>(new ConfigurationDisplayableToken());
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
