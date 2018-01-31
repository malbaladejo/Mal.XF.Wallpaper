using System;
using System.Collections.Generic;
using System.Text;
using Mal.XF.Wallpaper.Services;

namespace Mal.XF.Wallpaper.StateMachines
{
    internal class ScheduleBackgroundUpdateServiceTo8AmState : StateBase
    {
        private readonly IBackgroundUpdateService backgroundUpdateService;

        public ScheduleBackgroundUpdateServiceTo8AmState(IBackgroundUpdateService backgroundUpdateService)
        {
            this.backgroundUpdateService = backgroundUpdateService;
        }
        public override bool IsValid() => true;

        public override void Execute() => this.backgroundUpdateService.StartNext8Am();
    }
}
