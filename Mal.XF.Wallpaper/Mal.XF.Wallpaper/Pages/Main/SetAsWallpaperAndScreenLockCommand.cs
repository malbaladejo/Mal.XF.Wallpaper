using System;
using System.Threading.Tasks;
using Mal.XF.Wallpaper.Services;
using Prism.Commands;

namespace Mal.XF.Wallpaper.Pages.Main
{
    internal class SetAsWallpaperAndScreenLockCommand : DelegateCommandBase
    {
        private readonly IWallpaperService bingWallpaperService;
        private readonly Action<bool, string> setIsBusy;

        public SetAsWallpaperAndScreenLockCommand(IWallpaperService bingWallpaperService, Action<bool, string> setIsBusy)
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
                this.setIsBusy(true, "Set as wallpaper & screen lock...");
                this.RaiseCanExecuteChanged();
                await Task.WhenAll(this.bingWallpaperService.SetImageAsWallpaperAsync(filePath),
                    this.bingWallpaperService.SetImageAsScreenLockAsync(filePath));
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