using Android.App;
using Android.Graphics;
using Mal.XF.Wallpaper.Services;
using System.Threading.Tasks;

namespace Mal.XF.Wallpaper.Droid.Services
{
    internal class AndroidWallpaperService : IWallpaperService
    {
        private readonly WallpaperManager wallpaperManager;

        public AndroidWallpaperService()
        {
            this.wallpaperManager = WallpaperManager.GetInstance(Application.Context);
        }

        public Task SetImageAsScreenLockAsync(string imagePath) =>
            this.SetImageAsync(imagePath, WallpaperManagerFlags.Lock);

        public Task SetImageAsWallpaperAsync(string imagePath) =>
            this.SetImageAsync(imagePath, WallpaperManagerFlags.System);

        private Task SetImageAsync(string imagePath, WallpaperManagerFlags which)
        {
            var bmp = BitmapFactory.DecodeFile(imagePath);
            return Task.Run(() =>
                this.wallpaperManager.SetBitmap(bmp, new Rect(0, 0, bmp.Width, bmp.Height), false, which));
        }
    }
}