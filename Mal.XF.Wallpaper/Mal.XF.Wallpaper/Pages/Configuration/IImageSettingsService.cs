using System.Threading.Tasks;
using Mal.XF.Wallpaper.Models;

namespace Mal.XF.Wallpaper.Pages.Configuration
{
    internal interface IImageSettingsService
    {
        Task SaveSettingsAsync(RefreshImageType type);
        RefreshImageType GetSettings();
    }
}