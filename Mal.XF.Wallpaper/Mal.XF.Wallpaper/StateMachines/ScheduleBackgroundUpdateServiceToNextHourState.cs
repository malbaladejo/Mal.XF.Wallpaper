using System;
using System.Collections.Generic;
using System.Text;
using Mal.XF.Wallpaper.Services;

namespace Mal.XF.Wallpaper.StateMachines
{
    /// <summary>
    /// Etat qui pa
    /// </summary>
    internal class ScheduleBackgroundUpdateServiceToNextHourState : StateBase
    {
        private readonly IBackgroundUpdateService backgroundUpdateService;

        public ScheduleBackgroundUpdateServiceToNextHourState(IBackgroundUpdateService backgroundUpdateService)
        {
            this.backgroundUpdateService = backgroundUpdateService;
        }

        public override bool IsValid() => true;

        public override void Execute() => this.backgroundUpdateService.StartNextHour();
    }
}
