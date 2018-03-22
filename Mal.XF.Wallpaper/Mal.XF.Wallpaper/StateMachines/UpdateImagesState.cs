using Mal.XF.Infra.Log;
using Mal.XF.Wallpaper.Services;
using System;

namespace Mal.XF.Wallpaper.StateMachines
{
    /// <summary>
    /// Etat qui met à jour les Wallpaper et ScreenLock.
    /// </summary>
    internal class UpdateImagesState : ActionStateBase
    {
        private readonly IWallpaperBackgroundService wallpaperBackgroundService;
        private readonly ILogger logger;

        public UpdateImagesState(IWallpaperBackgroundService wallpaperBackgroundService, ILogger logger)
        {
            this.wallpaperBackgroundService = wallpaperBackgroundService;
            this.logger = logger;
        }

        protected override void DoAction()
        {
            try
            {
                this.logger.Debug($"Wallpaper & ScreenLock updating");
                this.wallpaperBackgroundService.UpdateImagesAsync().Wait();
                this.logger.Debug($"Wallpaper & ScreenLock updated");
            }
            catch (Exception e)
            {
                this.logger.Error(e.Message, e);
                throw;
            }
        }        
    }
}
