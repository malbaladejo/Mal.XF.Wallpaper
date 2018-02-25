﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Mal.XF.Infra.Net;
using Mal.XF.Infra.Log;

namespace Mal.XF.Wallpaper.StateMachines
{
    /// <summary>
    /// Etat qui vérifie si le Wifi est disponible.
    /// </summary>
    internal class IsWifiEnabledState : StateBase
    {
        private readonly INetworkService networkService;
        private readonly ILogger logger;

        public IsWifiEnabledState(INetworkService networkService, ILogger logger)
        {
            this.networkService = networkService;
            this.logger = logger;
        }

        public override bool IsValid()
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

        public override void Execute()
        {
            // Nothing to do
        }
    }
}
