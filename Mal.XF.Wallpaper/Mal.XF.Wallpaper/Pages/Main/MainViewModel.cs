using Mal.XF.Wallpaper.Models;
using Mal.XF.Wallpaper.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Mal.XF.Infra.Log;
using Xamarin.Forms.Internals;

namespace Mal.XF.Wallpaper.Pages.Main
{
    internal class MainViewModel : BindableBase, INavigationAware
    {
        private readonly IBingWallpaperService bingWallpaperService;
        private readonly ILogger logger;
        private readonly IReadOnlyCollection<DelegateCommandBase> commands;

        public MainViewModel(IBingWallpaperRepository bingWallpaperService, IWallpaperService wallpaperService, ILogger logger)
        {
            this.bingWallpaperService = bingWallpaperService;
            this.logger = logger;
            this.setAsWallpaperCommand = new SetAsWallpaperCommand(wallpaperService, this.SetIsBusy);
            this.setAsScreenLockCommand = new SetAsScreenLockCommand(wallpaperService, this.SetIsBusy);
            this.setAsWallpaperAndScreenLockCommand = new SetAsWallpaperAndScreenLockCommand(wallpaperService, this.SetIsBusy);

            this.commands = new List<DelegateCommandBase>
            {
                this.setAsWallpaperCommand,
                this.setAsScreenLockCommand,
                this.setAsWallpaperAndScreenLockCommand
            };
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

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            this.logger.Info($"{nameof(MainViewModel)}.{nameof(INavigationAware.OnNavigatedFrom)}");
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            this.logger.Info($"{nameof(MainViewModel)}.{nameof(INavigationAware.OnNavigatedTo)}");
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
                var images = await this.bingWallpaperService.GetImagesAsync();

                this.TodayImage = images[(int)RefreshImageType.ImageOfTheDay];

                this.SetIsBusy(true, "Downloading today image...");
                this.TodayImagePath = await this.bingWallpaperService.DownloadImageAsync(this.TodayImage);
            }
            catch (Exception e)
            {
                this.Message = $"Error:{e.Message}";
            }
            finally
            {
                this.SetIsBusy(false, "");
            }
        }

        private void RefreshCommands()
        {
            this.commands.ForEach(c => c.RaiseCanExecuteChanged());
        }
    }
}
