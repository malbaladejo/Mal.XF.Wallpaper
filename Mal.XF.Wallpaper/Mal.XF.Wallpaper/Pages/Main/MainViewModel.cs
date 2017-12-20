using Mal.XF.Wallpaper.Services;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Linq;
using System.Windows.Input;
using Prism.Commands;

namespace Mal.XF.Wallpaper.Pages.Main
{
    internal class MainViewModel : BindableBase, INavigationAware
    {
        private readonly IBingWallpaperService bingWallpaperService;

        public MainViewModel(IBingWallpaperService bingWallpaperService)
        {
            this.bingWallpaperService = bingWallpaperService;
            this.SetAsWallpaperCommand = new DelegateCommand<string>(this.SetAsWallpapper, this.CanSetAsWallpapper);
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
                var bingImages = (await this.bingWallpaperService.GetBinImagesAsync(2)).ToList();
                this.Message = "Clear images...";
                await this.bingWallpaperService.ClearImagesAsync(bingImages);
                this.Message = "Downloading image 1...";
                var imagePath = await this.bingWallpaperService.DownloadImageAsync(bingImages[0]);
                this.Message = "Set image as wallpapper...";
                await this.bingWallpaperService.SetImageAsWallpaperAsync(imagePath);

                this.Message = "Downloading image 2...";
                imagePath = await this.bingWallpaperService.DownloadImageAsync(bingImages[1]);
                this.Message = "Set image as screen lock...";
                await this.bingWallpaperService.SetImageAsScreenLockAsync(imagePath);
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

        public ICommand SetAsWallpaperCommand { get; }
        public ICommand SetAsScreenLockCommand { get; }
        public ICommand SetAsWallpaperAndScreenLockCommand { get; }

    }
}
