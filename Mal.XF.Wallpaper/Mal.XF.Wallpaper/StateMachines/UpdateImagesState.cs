using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Mal.XF.Wallpaper.Services;

namespace Mal.XF.Wallpaper.StateMachines
{
    /// <summary>
    /// Etat qui met à jour les Wallpaper et ScreenLock.
    /// </summary>
    internal class UpdateImagesState : StateBase
    {
        private readonly IWallpaperBackgroundService wallpaperBackgroundService;

        public UpdateImagesState(IWallpaperBackgroundService wallpaperBackgroundService)
        {
            this.wallpaperBackgroundService = wallpaperBackgroundService;
        }

        public override bool IsValid() => true;

        public override void Execute() => this.wallpaperBackgroundService.UpdateImagesAsync().Wait();
    }
}
