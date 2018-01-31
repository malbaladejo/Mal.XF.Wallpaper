using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Mal.XF.Infra.Net;

namespace Mal.XF.Wallpaper.StateMachines
{
    /// <summary>
    /// Etat qui vérifie si le Wifi est disponible.
    /// </summary>
    internal class IsWifiEnabledState : StateBase
    {
        private readonly INetworkService networkService;

        public IsWifiEnabledState(INetworkService networkService)
        {
            this.networkService = networkService;
        }

        public override bool IsValid() => networkService.IsWifiEnabled();

        public override void Execute()
        {
            // Nothing to do
        }
    }
}
