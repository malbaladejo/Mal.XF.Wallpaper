using Mal.XF.Wallpaper.Services;
using Prism.Mvvm;
using Prism.Navigation;
using System;

namespace Mal.XF.Wallpaper.Pages.Main
{
    internal class MainViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService navigationService;
        private readonly IBingWallpaperService bingWallpaperService;

        public MainViewModel(INavigationService navigationService, IBingWallpaperService bingWallpaperService)
        {
            this.navigationService = navigationService;
            this.bingWallpaperService = bingWallpaperService;
        }

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        private string message;
        public string Message
        {
            get { return message; }
            set { SetProperty(ref message, value); }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            this.Load();
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {

        }

        private async void Load()
        {
            try
            {
                this.IsBusy = true;
                this.Message = "Getting metadata...";
                var bingImage = await this.bingWallpaperService.GetBinImageAsync();
                this.Message = "Downloading image...";
                var imagePath = await this.bingWallpaperService.DownloadImageAsync(bingImage);
                this.Message = "Set image as wallpapper...";
                this.bingWallpaperService.SetImageAsWallpaper(imagePath);
                this.Message = "Done";
            }
            catch (Exception e)
            {
                this.Message = $"Error:{e.Message}";
            }
            finally
            {
                this.IsBusy = false;
            }
        }

    }
}
