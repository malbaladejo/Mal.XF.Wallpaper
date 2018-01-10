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
        public ConfigurationViewModel(ISettingsService settingsService)
        {
            var settings = settingsService.GetSettings();
            this.WallpaperConfiguration = new ConfigurationItem(new WallpaperSettingsService(settingsService, settings));
            this.ScreenLockConfiguration = new ConfigurationItem(new ScreenLockSettingsService(settingsService, settings));
        }

        public ConfigurationItem WallpaperConfiguration { get; }
        public ConfigurationItem ScreenLockConfiguration { get; }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            this.WallpaperConfiguration.LoadSettingsAsync();
            this.ScreenLockConfiguration.LoadSettingsAsync();
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}
