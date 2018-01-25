using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Mal.XF.Wallpaper.Models;

namespace Mal.XF.Wallpaper.Services
{
    internal interface ILocalStorageService
    {
        Settings GetSettings();

        Task SaveSettingsAsync(Settings settings);

        BingImageMetadata GetMetadata();

        Task SaveMetadataAsync(BingImageMetadata metadata);
    }
}
