using System.Threading.Tasks;
using Mal.XF.Wallpaper.Models;
using Mal.XF.Wallpaper.Services;

namespace Mal.XF.Wallpaper.Pages.Configuration
{
    internal class ScreenLockSettingsService : IImageSettingsService
    {
        private readonly ILocalStorageService localStorageService;
        private readonly Settings settings;

        public ScreenLockSettingsService(ILocalStorageService localStorageService, Settings settings)
        {
            this.localStorageService = localStorageService;
            this.settings = settings;
        }

        public RefreshImageType GetSettings() => this.settings.RefreshScreenLock;

        public Task SaveSettingsAsync(RefreshImageType type)
        {
            this.settings.RefreshScreenLock = type;
            return this.localStorageService.SaveSettingsAsync(this.settings);
        }
    }
}