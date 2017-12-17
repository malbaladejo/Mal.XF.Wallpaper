using Android.Content;
using Mal.XF.Infra.Extensions;
using Mal.XF.Wallpaper.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mal.XF.Wallpaper.Droid.Services
{
    internal class AndroidFileService : IFileService
    {
        public string GetImageDirectoryPath() => "imgDir";

        public async Task<IReadOnlyCollection<string>> GetFilesAsync()
        {
            var cw = new ContextWrapper(Android.App.Application.Context);
            var directory = cw.GetDir(this.GetImageDirectoryPath(), FileCreationMode.Private);
            var files = await directory.ListFilesAsync();

            return files.Select(f => f.AbsolutePath).ToReadOnlyCollection();
        }

        public Task RemoveFileAsync(string filePath)
        {
            return Task.Run(() =>
            {
                var file = new Java.IO.File(filePath);
                var result = file.Delete();                
            });
        }
    }
}