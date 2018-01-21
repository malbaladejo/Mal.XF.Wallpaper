using Android.Content;
using Android.Graphics;
using Mal.XF.Infra.IO;
using Mal.XF.Wallpaper.Models;
using Mal.XF.Wallpaper.Services;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Mal.XF.Wallpaper.Droid.Services
{
    internal class AndroidDownloadService : IDownloadService
    {
        private readonly IFileService fileService;

        public AndroidDownloadService(IFileService fileService)
        {
            this.fileService = fileService;
        }

        public async Task<string> DownloadImageAsync(BingImage image, string imageDirectory)
        {
            var file = this.GetFile(image, imageDirectory);

            if (!await this.IsFileExist(imageDirectory, file.Name))
                await DownloadFileAsync(image, file.AbsolutePath);

            return file.AbsolutePath;
        }

        public string GetFileName(BingImage image) => image.GetMobileFileName();

        private static async Task DownloadFileAsync(BingImage image, string absolutePath)
        {
            using (var webClient = new WebClient())
            {
                var imageUrl = image.GetMobileFullUrl();
                using (var stream = await webClient.OpenReadTaskAsync(imageUrl))
                    SaveFile(absolutePath, stream);
            }
        }

        private static void SaveFile(string absolutePath, Stream stream)
        {
            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                var imgByteArray = ms.ToArray();
                var bm = BitmapFactory.DecodeByteArray(imgByteArray, 0, imgByteArray.Length);

                using (var os = new FileStream(absolutePath, FileMode.Create))
                    bm.Compress(Bitmap.CompressFormat.Png, 100, os);
            }
        }

        private Java.IO.File GetFile(BingImage image, string imageDirectory)
        {
            var cw = new ContextWrapper(Android.App.Application.Context);
            var directory = cw.GetDir(imageDirectory, FileCreationMode.Private);
            return new Java.IO.File(directory, this.GetFileName(image));
        }

        private async Task<bool> IsFileExist(string imageDirectory, string fileName)
        {
            var files = await this.fileService.GetFilesAsync(imageDirectory);
            return files.Any(f => f.EndsWith(fileName));
        }
    }
}