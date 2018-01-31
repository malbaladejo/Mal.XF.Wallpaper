using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Mal.XF.Wallpaper.Services;

namespace Mal.XF.Wallpaper.StateMachines
{
    /// <summary>
    /// Etat qui verifie si la mise à jour des images à déjà était faite aujourd'hui.
    /// </summary>
    internal class IsUpdateRequiredBaseOnLastUpdateState : StateBase
    {
        private readonly ILocalStorageService localStorageService;

        public IsUpdateRequiredBaseOnLastUpdateState(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }

        public override bool IsValid() => this.localStorageService.GetSettings().LastUpdate.Date != DateTime.Now.Date;

        public override void Execute()
        {
            // Nothing to do.
        }
    }
}
