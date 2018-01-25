using System;
using System.Collections.Generic;
using System.Text;
using Mal.XF.Wallpaper.Services;
using Prism.Mvvm;
using Prism.Navigation;

namespace Mal.XF.Wallpaper.Pages.Configuration
{
    internal class ConfigurationViewModel : BindableBase, INavigationAware
    {
        private readonly ILocalStorageService localStorageService;
        private readonly IBackgroundUpdateService backgroundUpdateService;
        private bool isUpdateRequired;

        public ConfigurationViewModel(ILocalStorageService localStorageService, IBackgroundUpdateService backgroundUpdateService)
        {
            this.localStorageService = localStorageService;
            this.backgroundUpdateService = backgroundUpdateService;
            var settings = localStorageService.GetSettings();
            this.WallpaperConfiguration = new ConfigurationItem(new WallpaperSettingsService(localStorageService, settings));
            this.ScreenLockConfiguration = new ConfigurationItem(new ScreenLockSettingsService(localStorageService, settings));

            this.WallpaperConfiguration.SettingsChanged += this.SettingsChanged;
            this.ScreenLockConfiguration.SettingsChanged += this.SettingsChanged;
        }

        private void SettingsChanged(object sender, EventArgs e)
        {
            if (this.isUpdateRequired == this.localStorageService.GetSettings().IsUpdateRequired)
                return;

            this.isUpdateRequired = this.localStorageService.GetSettings().IsUpdateRequired;

            if (isUpdateRequired)
                this.backgroundUpdateService.Start();
            else
                this.backgroundUpdateService.Stop();
        }

        public ConfigurationItem WallpaperConfiguration { get; }
        public ConfigurationItem ScreenLockConfiguration { get; }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            this.WallpaperConfiguration.LoadSettings();
            this.ScreenLockConfiguration.LoadSettings();

            this.isUpdateRequired = this.localStorageService.GetSettings().IsUpdateRequired;
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}
