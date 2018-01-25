using Android.App;
using Mal.XF.Wallpaper.Services;

namespace Mal.XF.Wallpaper.Droid.Services
{
    internal class AndroidBackgroundUpdateService : BackgroundUpdateServiceBase
    {
        private readonly AlarmManagerBroadcastReceiver alarmManagerBroadcastReceiver;

        public AndroidBackgroundUpdateService(ILocalStorageService localStorageService)
            : base(localStorageService)
        {
            this.alarmManagerBroadcastReceiver = new AlarmManagerBroadcastReceiver();
        }

        protected override void StartService()
        {
            this.alarmManagerBroadcastReceiver.SetAlarm(Application.Context);
        }

        public override void Stop()
        {
            this.alarmManagerBroadcastReceiver.CancelAlarm(Application.Context);
        }
    }
}