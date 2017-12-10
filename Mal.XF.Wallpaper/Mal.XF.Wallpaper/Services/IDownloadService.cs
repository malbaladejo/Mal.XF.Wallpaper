using Mal.XF.Wallpaper.Models;
using System.Threading.Tasks;

namespace Mal.XF.Wallpaper.Services
{
    internal interface IDownloadService
    {
        Task<string> DownloadImageAsync(BingImage image);
    }
}
