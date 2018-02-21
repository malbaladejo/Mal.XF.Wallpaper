using System;
using Mal.XF.Infra.Log;
using Mal.XF.Wallpaper.Services;
using Prism.Commands;

namespace Mal.XF.Wallpaper.Pages.Main
{
    internal class SetAsScreenLockCommand : DelegateCommandBase
    {
        private readonly IWallpaperService bingWallpaperService;
        private readonly Action<bool, string> setIsBusy;
        private readonly ILogger logger;

        public SetAsScreenLockCommand(IWallpaperService bingWallpaperService, Action<bool, string> setIsBusy, ILogger logger)
        {
            this.bingWallpaperService = bingWallpaperService;
            this.setIsBusy = setIsBusy;
            this.logger = logger;
            this.IsActive = true;
        }

        public bool CanExecute(string filePath) => this.IsActive && !string.IsNullOrWhiteSpace(filePath);

        public async void Execute(string filePath)
        {
            try
            {
                this.IsActive = false;
                this.setIsBusy(true, "Set as screen lock...");
                this.RaiseCanExecuteChanged();
                this.logger.Debug($"{nameof(SetAsScreenLockCommand)} begin");
                await this.bingWallpaperService.SetImageAsScreenLockAsync(filePath);
                this.logger.Debug($"{nameof(SetAsScreenLockCommand)} end");
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