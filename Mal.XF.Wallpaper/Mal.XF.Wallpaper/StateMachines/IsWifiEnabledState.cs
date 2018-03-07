using Mal.XF.Infra.Log;
using Mal.XF.Infra.Net;
using System;

namespace Mal.XF.Wallpaper.StateMachines
{
    /// <summary>
    /// Etat qui vérifie si le Wifi est disponible.
    /// </summary>
    internal class IsWifiEnabledState : SwitchStateBase
    {
        private readonly INetworkService networkService;
        private readonly ILogger logger;

        public IsWifiEnabledState(INetworkService networkService, ILogger logger)
        {
            this.networkService = networkService;
            this.logger = logger;
        }

        protected override bool IsValid()
        {
            try
            {
                var isValid = networkService.IsWifiEnabled();
                this.logger.Debug($"Is Wifi Enabled: {isValid}");

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
