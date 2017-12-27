using System;
using Mal.XF.Wallpaper.Services;
using Prism.Commands;

namespace Mal.XF.Wallpaper.Pages.Main
{
    internal class SetAsScreenLockCommand : DelegateCommandBase
    {
        private readonly IBingWallpaperService bingWallpaperService;
        private readonly Action<bool, string> setIsBusy;

        public SetAsScreenLockCommand(IBingWallpaperService bingWallpaperService, Action<bool, string> setIsBusy)
        {
            this.bingWallpaperService = bingWallpaperService;
            this.setIsBusy = setIsBusy;
            this.IsActive = true;
        }

        public bool CanExecute(string filePath) => this.IsActive && !string.IsNullOrWhiteSpace(filePath);

        public async void Execute(string filePath)
        {
            try
            {
                this.IsActive = false;
                this.setIsBusy(true, "Set as screnn lock...");
                this.RaiseCanExecuteChanged();
                await this.bingWallpaperService.SetImageAsScreenLockAsync(filePath);
            }
            finally
            {
                this.IsActive = true;
                this.setIsBusy(false, "");
                this.RaiseCanExecuteChanged();
            }
        }

        protected override void Execute(object parameter) => this.Execute(parameter as string);

        protected override bool CanExecute(object parameter) => this.CanExecute(parameter as string);
    }
}