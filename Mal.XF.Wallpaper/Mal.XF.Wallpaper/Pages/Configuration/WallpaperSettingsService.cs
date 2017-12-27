using System.Threading.Tasks;
using Mal.XF.Wallpaper.Models;
using Mal.XF.Wallpaper.Services;

namespace Mal.XF.Wallpaper.Pages.Configuration
{
    internal class WallpaperSettingsService : IImageSettingsService
    {
        private readonly ISettingsService settingsService;
        private readonly Settings settings;

        public WallpaperSettingsService(ISettingsService settingsService, Settings settings)
        {
            this.settingsService = settingsService;
            this.settings = settings;
        }


        public RefreshImageType GetSettings() => this.settings.RefreshWallpaper;

        public Task SaveSettingsAsync(RefreshImageType type)
        {
            this.settings.RefreshWallpaper = type;
            return this.settingsService.SaveSettingsAsync(this.settings);
        }
    }
}