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
        private readonly ISettingsService settingsService;
        private readonly IBackgroundUpdateService backgroundUpdateService;
        private bool isUpdateRequired;

        public ConfigurationViewModel(ISettingsService settingsService, IBackgroundUpdateService backgroundUpdateService)
        {
            this.settingsService = settingsService;
            this.backgroundUpdateService = backgroundUpdateService;
            var settings = settingsService.GetSettings();
            this.WallpaperConfiguration = new ConfigurationItem(new WallpaperSettingsService(settingsService, settings));
            this.ScreenLockConfiguration = new ConfigurationItem(new ScreenLockSettingsService(settingsService, settings));

            this.WallpaperConfiguration.SettingsChanged += this.SettingsChanged;
            this.ScreenLockConfiguration.SettingsChanged += this.SettingsChanged;
        }

        private void SettingsChanged(object sender, EventArgs e)
        {
            if (this.isUpdateRequired == this.settingsService.GetSettings().IsUpdateRequired)
                return;

            this.isUpdateRequired = this.settingsService.GetSettings().IsUpdateRequired;

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

            this.isUpdateRequired = this.settingsService.GetSettings().IsUpdateRequired;
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}
