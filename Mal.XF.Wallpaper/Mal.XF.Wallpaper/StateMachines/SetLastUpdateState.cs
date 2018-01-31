using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Mal.XF.Wallpaper.Services;

namespace Mal.XF.Wallpaper.StateMachines
{
    /// <summary>
    /// Etat met à jour la date de mise à jour des images.
    /// </summary>
    internal class SetLastUpdateState : StateBase
    {
        private readonly ILocalStorageService localStorageService;

        public SetLastUpdateState(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }

        public override bool IsValid() => true;

        public override void Execute()
        {
            var settings =  this.localStorageService.GetSettings();
            settings.LastUpdate = DateTime.Now;
            this.localStorageService.SaveSettingsAsync(settings).Wait();
        }
    }
}
