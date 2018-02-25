using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Mal.XF.Wallpaper.Services;
using Mal.XF.Infra.Log;

namespace Mal.XF.Wallpaper.StateMachines
{
    /// <summary>
    /// Etat qui met à jour les Wallpaper et ScreenLock.
    /// </summary>
    internal class UpdateImagesState : StateBase
    {
        private readonly IWallpaperBackgroundService wallpaperBackgroundService;
        private readonly ILogger logger;

        public UpdateImagesState(IWallpaperBackgroundService wallpaperBackgroundService, ILogger logger)
        {
            this.wallpaperBackgroundService = wallpaperBackgroundService;
            this.logger = logger;
        }

        public override bool IsValid() => true;

        public override void Execute() {
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
