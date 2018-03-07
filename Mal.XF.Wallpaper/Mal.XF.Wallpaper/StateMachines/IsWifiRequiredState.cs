using Mal.XF.Infra.Log;
using Mal.XF.Wallpaper.Services;
using System;

namespace Mal.XF.Wallpaper.StateMachines
{
    /// <summary>
    /// Etat qui vérifie si la configuration indique que la mise à jour ne doit se faire que si le Wifi est disponible.
    /// </summary>
    internal class IsWifiRequiredState : SwitchStateBase
    {
        private readonly ILocalStorageService localStorageService;
        private readonly ILogger logger;

        public IsWifiRequiredState(ILocalStorageService localStorageService, ILogger logger)
        {
            this.localStorageService = localStorageService;
            this.logger = logger;
        }

        protected override bool IsValid()
        {
            try
            {
                var isValid = this.localStorageService.GetSettings().IsWifiRequired;
                this.logger.Debug($"Is Wifi Required: {isValid}");

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
