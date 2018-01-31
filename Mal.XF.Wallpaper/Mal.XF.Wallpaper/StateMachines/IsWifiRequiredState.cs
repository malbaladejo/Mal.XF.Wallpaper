using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Mal.XF.Wallpaper.Services;

namespace Mal.XF.Wallpaper.StateMachines
{
    /// <summary>
    /// Etat qui vérifie si la configuration indique que la mise à jour ne doit se faire que si le Wifi est disponible.
    /// </summary>
    internal class IsWifiRequiredState : StateBase
    {
        private readonly ILocalStorageService localStorageService;

        public IsWifiRequiredState(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }

        public override bool IsValid() => this.localStorageService.GetSettings().IsWifiRequired;

        public override void Execute()
        {
            // Nothing to do.
        }
    }
}
