using Mal.XF.Infra.Log;
using Mal.XF.Wallpaper.Services;
using System;

namespace Mal.XF.Wallpaper.StateMachines
{
    /// <summary>
    /// Etat qui vérifie si de nouvelles images sont disponibles.
    /// </summary>
    internal class IsNewImagesAvailableState : SwitchStateBase
    {
        private readonly IBingWallpaperService bingWallpaperService;
        private readonly ILocalStorageService localStorageService;
        private readonly ILogger logger;

        public IsNewImagesAvailableState(IBingWallpaperService bingWallpaperService, 
                                         ILocalStorageService localStorageService,
                                         ILogger logger)
        {
            this.bingWallpaperService = bingWallpaperService;
            this.localStorageService = localStorageService;
            this.logger = logger;
        }

        protected override bool IsValid()
        {
            try
            {
                var bingImages = this.bingWallpaperService.GetImagesAsync().Result;
                var metada = localStorageService.GetMetadata();

                var isValid = bingImages[0] != metada.Images[0];
                this.logger.Debug($"Has new images available:{isValid}");

                return isValid;
            }
            catch(Exception e)
            {
                this.logger.Error(e.Message, e);
                throw;
            }
        }
    }
}
