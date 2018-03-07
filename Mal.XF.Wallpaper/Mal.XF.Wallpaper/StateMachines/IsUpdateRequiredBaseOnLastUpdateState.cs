using Mal.XF.Infra.Log;
using Mal.XF.Wallpaper.Services;
using System;

namespace Mal.XF.Wallpaper.StateMachines
{
    /// <summary>
    /// Etat qui verifie si la mise à jour des images n'a pas déjà était faite aujourd'hui.
    /// </summary>
    internal class IsUpdateRequiredBaseOnLastUpdateState : SwitchStateBase
    {
        private readonly ILocalStorageService localStorageService;
        private readonly ILogger logger;

        public IsUpdateRequiredBaseOnLastUpdateState(ILocalStorageService localStorageService, ILogger logger)
        {
            this.localStorageService = localStorageService;
            this.logger = logger;
        }

        protected override bool IsValid()
        {
            try
            {
                var isValid = this.localStorageService.GetSettings().LastUpdate.Date != DateTime.Now.Date;
                this.logger.Debug($"Last update on {this.localStorageService.GetSettings().LastUpdate.Date}: {isValid}");
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
