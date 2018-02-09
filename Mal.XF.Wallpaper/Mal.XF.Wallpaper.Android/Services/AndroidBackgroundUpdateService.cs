using Android.App;
using Mal.XF.Infra.Log;
using Mal.XF.Wallpaper.Services;

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

        protected override void StartServiceNext8Am() => this.alarmManagerBroadcastReceiver.SetAlarm(Application.Context);

        protected override void StartServiceNextHour() => this.alarmManagerBroadcastReceiver.SetAlarmNextHour(Application.Context);

        public override void Stop() => this.alarmManagerBroadcastReceiver.CancelAlarm(Application.Context);
    }
}