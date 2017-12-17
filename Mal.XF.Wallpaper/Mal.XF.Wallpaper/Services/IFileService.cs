using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mal.XF.Wallpaper.Services
{
    interface IFileService
    {
        string GetImageDirectoryPath();
        Task<IReadOnlyCollection<string>> GetFilesAsync();
        Task RemoveFileAsync(string filePath);
    }
}
