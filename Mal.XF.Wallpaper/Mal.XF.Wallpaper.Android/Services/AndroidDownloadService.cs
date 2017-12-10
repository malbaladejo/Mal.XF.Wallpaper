using Android.Content;
using Android.Graphics;
using Mal.XF.Wallpaper.Models;
using Mal.XF.Wallpaper.Services;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Mal.XF.Wallpaper.Droid.Services
{
    internal class AndroidDownloadService : IDownloadService
    {
        public async Task<string> DownloadImageAsync(BingImage image)
        {
            using (var webClient = new WebClient())
            {
                var imageUrl = image.GetMobileFullUrl();
                using (var stream = await webClient.OpenReadTaskAsync(imageUrl))
                {
                    var cw = new ContextWrapper(Android.App.Application.Context);
                    var directory = cw.GetDir("imgDir", FileCreationMode.Private);
                    var path = new Java.IO.File(directory, "bingWallpapper.png");

                    using (var ms = new MemoryStream())
                    {
                        stream.CopyTo(ms);
                        var imgByteArray = ms.ToArray();
                        var bm = BitmapFactory.DecodeByteArray(imgByteArray, 0, imgByteArray.Length);

                        using (var os = new FileStream(path.AbsolutePath, FileMode.Create))
                            bm.Compress(Bitmap.CompressFormat.Png, 100, os);

                        return path.AbsolutePath;
                    }
                }
            }
        }
    }
}