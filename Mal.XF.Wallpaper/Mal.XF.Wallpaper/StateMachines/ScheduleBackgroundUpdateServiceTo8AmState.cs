using System;
using System.Collections.Generic;
using System.Text;
using Mal.XF.Wallpaper.Services;
using Mal.XF.Infra.Log;

namespace Mal.XF.Wallpaper.StateMachines
{
    internal class ScheduleBackgroundUpdateServiceTo8AmState : StateBase
    {
        private readonly IBackgroundUpdateService backgroundUpdateService;
        private readonly ILogger logger;

        public ScheduleBackgroundUpdateServiceTo8AmState(IBackgroundUpdateService backgroundUpdateService, ILogger logger)
        {
            this.backgroundUpdateService = backgroundUpdateService;
            this.logger = logger;
        }
        public override bool IsValid() => true;

        public override void Execute()
        {
            try
            {
                this.logger.Debug($"Schedule alarm at next 8am");
                this.backgroundUpdateService.StartNext8Am();
            }
            catch (Exception e)
            {
                this.logger.Error(e.Message, e);
                throw;
            }
        }
    }
}
