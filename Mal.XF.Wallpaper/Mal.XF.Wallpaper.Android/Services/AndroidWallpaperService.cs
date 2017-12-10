using Android.App;
using Android.Graphics;
using Mal.XF.Wallpaper.Services;

namespace Mal.XF.Wallpaper.Droid.Services
{
    internal class AndroidWallpaperService : IWallpaperService
    {
        public void SetImageAsWallpaper(string imagePath)
        {
            var bmp = BitmapFactory.DecodeFile(imagePath);
            var m = WallpaperManager.GetInstance(Android.App.Application.Context);
            m.SetBitmap(bmp);
        }
    }
}