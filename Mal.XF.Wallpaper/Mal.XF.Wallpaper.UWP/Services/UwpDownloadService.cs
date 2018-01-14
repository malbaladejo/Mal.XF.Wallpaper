using Mal.XF.Wallpaper.Models;
using Mal.XF.Wallpaper.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;

namespace Mal.XF.Wallpaper.UWP.Services
{
    internal class UwpDownloadService : IDownloadService
    {
        private readonly StorageFolder folder;
        public UwpDownloadService()
        {
            this.folder = ApplicationData.Current.LocalFolder;
        }

        public async Task<string> DownloadImageAsync(BingImage image, string imageDirectory)
        {
            if (!await this.IsFileExist(this.GetFileName(image)))
                await this.DownloadFileAsync(image);

            return this.GetFileName(image); ;
        }

        private async Task<bool> IsFileExist(string fileName)
        {
            var files = await this.folder.GetFilesAsync();
            return files.Any(f => f.Name == fileName);
        }

        private async Task DownloadFileAsync(BingImage image)
        {
            var source = new Uri(image.GetFullUrl());

            var destinationFile = await this.folder.CreateFileAsync(this.GetFileName(image), CreationCollisionOption.ReplaceExisting);

            var downloader = new BackgroundDownloader();
            var download = downloader.CreateDownload(source, destinationFile);

            await download.StartAsync().AsTask();
        }

        public string GetFileName(BingImage image) => image.GetFileName();
    }
}