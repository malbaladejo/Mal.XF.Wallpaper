using Mal.XF.Infra.Log;
using Mal.XF.Wallpaper.Services;
using System;

namespace Mal.XF.Wallpaper.StateMachines
{
    /// <summary>
    /// Etat qui vérifie si la configuration nécessite une mise à jour des images.
    /// </summary>
    internal class IsUpdateRequiredBaseOnSettingsState : SwitchStateBase
    {
        private readonly ILocalStorageService localStorageService;
        private readonly ILogger logger;

        public IsUpdateRequiredBaseOnSettingsState(ILocalStorageService localStorageService, ILogger logger)
        {
            this.localStorageService = localStorageService;
            this.logger = logger;
        }

        protected override bool IsValid()
        {
            try
            {
                var isValid = this.localStorageService.GetSettings().IsUpdateRequired;
                this.logger.Debug($"Is Update Required: {isValid}");

                return isValid;
            }
            catch (Exception e)
            {
                this.logger.Error(e.Message, e);
                throw;
            }
        }
    }
}
