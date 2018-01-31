using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Mal.XF.Wallpaper.Services;

namespace Mal.XF.Wallpaper.StateMachines
{
    /// <summary>
    /// Etat qui vérifie si de nouvelles images sont disponibles.
    /// </summary>
    internal class IsNewImagesAvailableState : StateBase
    {
        private readonly IBingWallpaperService bingWallpaperService;
        private readonly ILocalStorageService localStorageService;

        public IsNewImagesAvailableState(IBingWallpaperService bingWallpaperService, ILocalStorageService localStorageService)
        {
            this.bingWallpaperService = bingWallpaperService;
            this.localStorageService = localStorageService;
        }

        public override bool IsValid()
        {
            var bingImages = this.bingWallpaperService.GetImagesAsync().Result;
            var metada = localStorageService.GetMetadata();
            return bingImages[0] == metada.Images[0];
        }

        public override void Execute()
        {
            // Nothing to do
        }
    }
}
