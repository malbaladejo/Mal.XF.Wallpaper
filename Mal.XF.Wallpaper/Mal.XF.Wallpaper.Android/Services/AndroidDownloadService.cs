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
            var file = GetFile(image, imageDirectory);

            if (!file.Exists())
                await DownloadFileAsync(image, file);

            return file.AbsolutePath;
        }

        private static async Task DownloadFileAsync(BingImage image, Java.IO.File file)
        {
            using (var webClient = new WebClient())
            {
                var imageUrl = image.GetMobileFullUrl();
                using (var stream = await webClient.OpenReadTaskAsync(imageUrl))
                    SaveFile(file, stream);
            }
        }

        private static void SaveFile(Java.IO.File file, Stream stream)
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

        private static Java.IO.File GetFile(BingImage image, string imageDirectory)
        {
            var cw = new ContextWrapper(Android.App.Application.Context);
            var directory = cw.GetDir(imageDirectory, FileCreationMode.Private);
            return new Java.IO.File(directory, image.GetFileName());
        }
    }
}