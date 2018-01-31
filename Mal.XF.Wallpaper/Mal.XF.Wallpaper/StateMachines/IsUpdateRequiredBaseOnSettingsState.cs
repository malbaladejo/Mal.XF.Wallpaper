using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Mal.XF.Wallpaper.Services;

namespace Mal.XF.Wallpaper.StateMachines
{
    /// <summary>
    /// Etat qui vérifie si la configuration nécessite une mise à jour des images.
    /// </summary>
    internal class IsUpdateRequiredBaseOnSettingsState : StateBase
    {
        private readonly ILocalStorageService localStorageService;

        public IsUpdateRequiredBaseOnSettingsState(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }

        public override bool IsValid() => this.localStorageService.GetSettings().IsUpdateRequired;

        public override void Execute()
        {
            // Nothing to do
        }
    }
}
