using Mal.XF.Infra.Log;
using Mal.XF.Wallpaper.Services;
using System;

namespace Mal.XF.Wallpaper.StateMachines
{
    /// <summary>
    /// Etat met à jour la date de mise à jour des images.
    /// </summary>
    internal class SetLastUpdateState : ActionStateBase
    {
        private readonly ILocalStorageService localStorageService;
        private readonly ILogger logger;

        public SetLastUpdateState(ILocalStorageService localStorageService, ILogger logger)
        {
            this.localStorageService = localStorageService;
            this.logger = logger;
        }

        protected override void DoAction()
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
