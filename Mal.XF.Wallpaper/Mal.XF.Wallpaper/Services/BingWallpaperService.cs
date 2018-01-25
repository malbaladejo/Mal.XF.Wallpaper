using System;
using Mal.XF.Wallpaper.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Mal.XF.Infra.Extensions;
using System.Linq;
using Mal.XF.Infra.IO;
using Mal.XF.Infra.Net;

namespace Mal.XF.Wallpaper.Services
{
    internal class BingWallpaperService : IBingWallpaperService
    {
        private const string BingUrl = "https://www.bing.com/HPImageArchive.aspx?format=js&idx=0&n=2";
        private const string ImageDirectoryPath = "imgDir";

        private readonly IDownloadService downloadService;
        private readonly IFileService fileService;

        public BingWallpaperService(IDownloadService downloadService,
                                    IFileService fileService)
        {
            this.downloadService = downloadService;
            this.fileService = fileService;
        }

        public async Task<IReadOnlyList<BingImage>> GetImagesAsync()
        {
            var webClient = new WebClient();

            var json = await webClient.DownloadStringAsync(BingUrl);
            var images = JsonConvert.DeserializeObject<BingImages>(json);

            await this.ClearImagesAsync(images.Images);
            return images.Images;
        }

        public Task<string> DownloadImageAsync(BingImage image)
            => this.downloadService.DownloadImageAsync(image, ImageDirectoryPath);

        private async Task ClearImagesAsync(IReadOnlyCollection<BingImage> images)
        {
            var existingFiles = await this.fileService.GetFilesAsync(ImageDirectoryPath);
            var filesToRemove = existingFiles.Where(f => images.All(i => !f.EndsWith(downloadService.GetFileName(i))));

            foreach (var file in filesToRemove)
                await this.fileService.RemoveFileAsync(file);
        }
    }
}
