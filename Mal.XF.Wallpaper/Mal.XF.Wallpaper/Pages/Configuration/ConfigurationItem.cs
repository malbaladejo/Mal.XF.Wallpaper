using Mal.XF.Wallpaper.Models;
using Prism.Mvvm;

namespace Mal.XF.Wallpaper.Pages.Configuration
{
    internal class ConfigurationItem : BindableBase
    {
        private readonly IImageSettingsService settingsService;

        public ConfigurationItem(IImageSettingsService settingsService)
        {
            this.settingsService = settingsService;
            this.PropertyChanged += ConfigurationItem_PropertyChanged;
        }

        public void LoadSettingsAsync()
        {
            var type = this.settingsService.GetSettings();
            this.None = type == RefreshImageType.None;
            this.ImageOfTheDay = type == RefreshImageType.ImageOfTheDay;
            this.ImageOfYesterday = type == RefreshImageType.ImageOfYesterday;
        }

        private void SaveSettingsAsync()
        {
            this.settingsService.SaveSettingsAsync(this.GetSettings());
        }

        private RefreshImageType GetSettings()
        {
            if (this.ImageOfYesterday)
                return RefreshImageType.ImageOfYesterday;

            if (this.ImageOfTheDay)
                return RefreshImageType.ImageOfTheDay;

            return RefreshImageType.None;
        }

        private void ConfigurationItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.None):
                    if (this.None)
                    {
                        this.ImageOfTheDay = false;
                        this.ImageOfYesterday = false;
                    }
                    break;

                case nameof(this.ImageOfTheDay):
                    if (this.ImageOfTheDay)
                    {
                        this.None = false;
                        this.ImageOfYesterday = false;
                    }
                    break;

                case nameof(this.ImageOfYesterday):
                    if (this.ImageOfYesterday)
                    {
                        this.ImageOfTheDay = false;
                        this.None = false;
                    }
                    break;
            }

            if (!this.ImageOfTheDay && !this.ImageOfYesterday)
                this.None = true;

            this.SaveSettingsAsync();
        }

        private bool none;
        public bool None
        {
            get { return none; }
            set { SetProperty(ref none, value); }
        }

        private bool imageOfTheDay;
        public bool ImageOfTheDay
        {
            get { return imageOfTheDay; }
            set { SetProperty(ref imageOfTheDay, value); }
        }

        private bool imageOfYesterday;
        public bool ImageOfYesterday
        {
            get { return imageOfYesterday; }
            set { SetProperty(ref imageOfYesterday, value); }
        }
    }
}