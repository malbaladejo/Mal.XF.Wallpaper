using Android.App;
using Mal.XF.Infra.Log;
using Mal.XF.Wallpaper.Services;
using System;

namespace Mal.XF.Wallpaper.Droid.Services
{
    internal class AndroidBackgroundUpdateService : BackgroundUpdateServiceBase
    {
        private readonly AlarmManagerBroadcastReceiver alarmManagerBroadcastReceiver;

        public AndroidBackgroundUpdateService(ILocalStorageService localStorageService, ILogger logger)
            : base(localStorageService, logger)
        {
            this.alarmManagerBroadcastReceiver = new AlarmManagerBroadcastReceiver();
        }

        protected override void StartService(DateTime dateTime) => this.alarmManagerBroadcastReceiver.SetAlarm(Application.Context, dateTime);

        public override void Stop() => this.alarmManagerBroadcastReceiver.CancelAlarm(Application.Context);
    }
}