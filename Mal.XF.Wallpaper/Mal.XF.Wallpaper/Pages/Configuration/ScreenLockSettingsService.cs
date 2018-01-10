using System.Threading.Tasks;
using Mal.XF.Wallpaper.Models;
using Mal.XF.Wallpaper.Services;

namespace Mal.XF.Wallpaper.Pages.Configuration
{
    internal class ScreenLockSettingsService : IImageSettingsService
    {
        private readonly ISettingsService settingsService;
        private readonly Settings settings;

        public ScreenLockSettingsService(ISettingsService settingsService, Settings settings)
        {
            this.settingsService = settingsService;
            this.settings = settings;
        }

        public RefreshImageType GetSettings() => this.settings.RefreshScreenLock;

        public Task SaveSettingsAsync(RefreshImageType type)
        {
            this.settings.RefreshScreenLock = type;
            return this.settingsService.SaveSettingsAsync(this.settings);
        }
    }
}