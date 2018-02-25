using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Mal.XF.Wallpaper.Services;
using Mal.XF.Infra.Log;

namespace Mal.XF.Wallpaper.StateMachines
{
    /// <summary>
    /// Etat met à jour la date de mise à jour des images.
    /// </summary>
    internal class SetLastUpdateState : StateBase
    {
        private readonly ILocalStorageService localStorageService;
        private readonly ILogger logger;

        public SetLastUpdateState(ILocalStorageService localStorageService, ILogger logger)
        {
            this.localStorageService = localStorageService;
            this.logger = logger;
        }

        public override bool IsValid() => true;

        public override void Execute()
        {
            try
            {                
                var settings = this.localStorageService.GetSettings();
                settings.LastUpdate = DateTime.Now;
                this.logger.Debug($"Settings Saving");
                this.localStorageService.SaveSettingsAsync(settings).Wait();
                this.logger.Debug($"Settings Saved");
            }
            catch (Exception e)
            {
                this.logger.Error(e.Message, e);
                throw;
            }
        }
    }
}
