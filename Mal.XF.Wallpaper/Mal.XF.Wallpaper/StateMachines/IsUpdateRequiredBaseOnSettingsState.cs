using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Mal.XF.Wallpaper.Services;
using Mal.XF.Infra.Log;

namespace Mal.XF.Wallpaper.StateMachines
{
    /// <summary>
    /// Etat qui vérifie si la configuration nécessite une mise à jour des images.
    /// </summary>
    internal class IsUpdateRequiredBaseOnSettingsState : StateBase
    {
        private readonly ILocalStorageService localStorageService;
        private readonly ILogger logger;

        public IsUpdateRequiredBaseOnSettingsState(ILocalStorageService localStorageService, ILogger logger)
        {
            this.localStorageService = localStorageService;
            this.logger = logger;
        }

        public override bool IsValid()
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

        public override void Execute()
        {
            // Nothing to do
        }
    }
}
