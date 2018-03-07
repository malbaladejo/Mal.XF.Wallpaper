using Mal.XF.Infra.Log;
using Mal.XF.Wallpaper.Services;
using System;

namespace Mal.XF.Wallpaper.StateMachines
{
    /// <summary>
    /// Etat qui pa
    /// </summary>
    internal class ScheduleBackgroundUpdateServiceToNextHourState : ActionStateBase
    {
        private readonly IBackgroundUpdateService backgroundUpdateService;
        private readonly ILogger logger;

        public ScheduleBackgroundUpdateServiceToNextHourState(IBackgroundUpdateService backgroundUpdateService, 
            ILogger logger)
        {
            this.backgroundUpdateService = backgroundUpdateService;
            this.logger = logger;
        }

        public override void Execute() {
            try
            {
                this.logger.Debug($"Schedule alarm at next hour.");
                this.backgroundUpdateService.StartNextHour();
            }
            catch (Exception e)
            {
                this.logger.Error(e.Message, e);
                throw;
            }
        }
    }
}
