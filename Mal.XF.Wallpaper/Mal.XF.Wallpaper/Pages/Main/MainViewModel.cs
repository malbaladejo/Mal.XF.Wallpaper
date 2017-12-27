using Mal.XF.Wallpaper.Services;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Mal.XF.Infra.Navigation;
using Mal.XF.Wallpaper.Models;
using Mal.XF.Wallpaper.Pages.Configuration;
using Prism.Commands;
using Xamarin.Forms.Internals;

namespace Mal.XF.Wallpaper.Pages.Main
{
    internal class MainViewModel : BindableBase, INavigationAware
    {
        private readonly IBingWallpaperService bingWallpaperService;
        private readonly INavigationService navigationService;
        private readonly IReadOnlyCollection<DelegateCommandBase> commands;

        public MainViewModel(IBingWallpaperService bingWallpaperService, INavigationService navigationService)
        {
            this.bingWallpaperService = bingWallpaperService;
            this.navigationService = navigationService;
            this.setAsWallpaperCommand = new SetAsWallpaperCommand(bingWallpaperService, this.SetIsBusy);
            this.setAsScreenLockCommand = new SetAsScreenLockCommand(bingWallpaperService, this.SetIsBusy);
            this.setAsWallpaperAndScreenLockCommand = new SetAsWallpaperAndScreenLockCommand(bingWallpaperService, this.SetIsBusy);

            this.commands = new List<DelegateCommandBase>
            {
                this.setAsWallpaperCommand,
                this.setAsScreenLockCommand,
                this.setAsWallpaperAndScreenLockCommand
            };

            this.ConfigurationCommand = new DelegateCommand(this.NavigateToConfiguration);
        }

        private void NavigateToConfiguration()
        {
            this.navigationService.NavigateByTokenAsync(new ConfigurationToken());
        }

        private void SetIsBusy(bool isBusy, string message)
        {
            this.IsBusy = isBusy;
            this.Message = message;
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

        private string todayImagePath;
        public string TodayImagePath
        {
            get { return todayImagePath; }
            set
            {
                if (SetProperty(ref todayImagePath, value))
                    this.RefreshCommands();
            }
        }

        private BingImage todayImage;

        public BingImage TodayImage
        {
            get { return todayImage; }
            set { SetProperty(ref todayImage, value); }
        }

        private readonly DelegateCommandBase setAsWallpaperCommand;
        public ICommand SetAsWallpaperCommand => setAsWallpaperCommand;

        private readonly DelegateCommandBase setAsScreenLockCommand;
        public ICommand SetAsScreenLockCommand => setAsScreenLockCommand;

        private readonly DelegateCommandBase setAsWallpaperAndScreenLockCommand;
        public ICommand SetAsWallpaperAndScreenLockCommand => setAsWallpaperAndScreenLockCommand;

        public ICommand ConfigurationCommand { get; }

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
                this.SetIsBusy(true, "Getting metadata...");
                var bingImages = (await this.bingWallpaperService.GetBinImagesAsync(2)).ToList();
                this.TodayImage = bingImages[0];
                this.Message = "Clear images...";
                await this.bingWallpaperService.ClearImagesAsync(bingImages);
                this.SetIsBusy(true, "Downloading today image...");
                this.TodayImagePath = await this.bingWallpaperService.DownloadImageAsync(bingImages[0]);
                //this.Message = "Set image as wallpapper...";
                //await this.bingWallpaperService.SetImageAsWallpaperAsync(imagePath);

                //this.Message = "Downloading image 2...";
                //imagePath = await this.bingWallpaperService.DownloadImageAsync(bingImages[1]);
                //this.Message = "Set image as screen lock...";
                //await this.bingWallpaperService.SetImageAsScreenLockAsync(imagePath);
                //this.Message = "Done";

                this.SetIsBusy(false, "");
            }
            catch (Exception e)
            {
                this.Message = $"Error:{e.Message}";
            }
        }

        private void RefreshCommands()
        {
            this.commands.ForEach(c => c.RaiseCanExecuteChanged());
        }
    }
}
