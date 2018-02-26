using Android.App;
using Mal.XF.Infra.Log;
using Mal.XF.Wallpaper.Services;
using System;

namespace Mal.XF.Wallpaper.Droid.Services
{
    internal class AndroidBackgroundUpdateService : BackgroundUpdateServiceBase
    {
        private readonly AlarmManagerFacade alarmManagerFacade;

        public AndroidBackgroundUpdateService(ILogger logger)
            : base(logger)
        {
            this.alarmManagerFacade = new AlarmManagerFacade(logger);
        }

        protected override void StartService(DateTime dateTime) => this.alarmManagerFacade.SetAlarm(Application.Context, dateTime);

        public override void Stop() => this.alarmManagerFacade.CancelAlarm(Application.Context);
    }
}