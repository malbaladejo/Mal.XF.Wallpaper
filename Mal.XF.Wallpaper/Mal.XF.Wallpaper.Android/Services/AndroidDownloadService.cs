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
        public async Task<string> DownloadImageAsync(BingImage image, string imageDirectory)
        {
            using (var webClient = new WebClient())
            {
                var imageUrl = image.GetMobileFullUrl();
                using (var stream = await webClient.OpenReadTaskAsync(imageUrl))
                {
                    var cw = new ContextWrapper(Android.App.Application.Context);
                    var directory = cw.GetDir(imageDirectory, FileCreationMode.Private);
                    var file = new Java.IO.File(directory, image.GetFileName());

                    if (!file.Exists())
                    {
                        using (var ms = new MemoryStream())
                        {
                            stream.CopyTo(ms);
                            var imgByteArray = ms.ToArray();
                            var bm = BitmapFactory.DecodeByteArray(imgByteArray, 0, imgByteArray.Length);

                            using (var os = new FileStream(file.AbsolutePath, FileMode.Create))
                                bm.Compress(Bitmap.CompressFormat.Png, 100, os);
                        }
                    }
                    return file.AbsolutePath;
                }
            }
        }
    }
}