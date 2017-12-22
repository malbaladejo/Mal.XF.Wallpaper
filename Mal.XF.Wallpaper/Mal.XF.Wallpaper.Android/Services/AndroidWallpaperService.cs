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

        public Task SetImageAsScreenLockAsync(string imagePath)
        {
            var bmp = GetBitmap(imagePath);

            return Task.Run(() =>
            this.wallpaperManager.SetBitmap(bmp, new Rect(0, 0, bmp.Width, bmp.Height), false, WallpaperManagerFlags.Lock));
        }

        public Task SetImageAsWallpaperAsync(string imagePath)
        {
            var bmp = GetBitmap(imagePath);
            return Task.Run(() =>
                this.wallpaperManager.SetBitmap(bmp, new Rect(0, 0, bmp.Width, bmp.Height), false, WallpaperManagerFlags.System));
        }

        private static Bitmap GetBitmap(string imagePath)
            => BitmapFactory.DecodeFile(imagePath);
    }
}