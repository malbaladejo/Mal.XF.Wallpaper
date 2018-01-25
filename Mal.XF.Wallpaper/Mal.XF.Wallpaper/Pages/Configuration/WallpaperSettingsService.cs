using System.Threading.Tasks;
using Mal.XF.Wallpaper.Models;
using Mal.XF.Wallpaper.Services;

namespace Mal.XF.Wallpaper.Pages.Configuration
{
    internal class WallpaperSettingsService : IImageSettingsService
    {
        private readonly ILocalStorageService localStorageService;
        private readonly Settings settings;

        public WallpaperSettingsService(ILocalStorageService localStorageService, Settings settings)
        {
            this.localStorageService = localStorageService;
            this.settings = settings;
        }
        
        public RefreshImageType GetSettings() => this.settings.RefreshWallpaper;

        public Task SaveSettingsAsync(RefreshImageType type)
        {
            this.settings.RefreshWallpaper = type;
            return this.localStorageService.SaveSettingsAsync(this.settings);
        }
    }
}