using Android.App;
using Android.Content;
using Mal.XF.Infra.Extensions;
using Mal.XF.Wallpaper.Services;
using System;

namespace Mal.XF.Wallpaper.Droid.Services
{
    [BroadcastReceiver]
    internal class AlarmManagerBroadcastReceiver : BroadcastReceiver
    {
        private readonly AlarmManager alarmManager;
        private readonly PendingIntent pendingIntent;
        private readonly IWallpaperBackgroundService backgroundUpdateService;

        public AlarmManagerBroadcastReceiver()
        {
            var currentIntent = new Intent(Application.Context, typeof(AlarmManagerBroadcastReceiver));
            this.alarmManager = (AlarmManager)Application.Context.GetSystemService(Context.AlarmService);
            this.pendingIntent = PendingIntent.GetBroadcast(Application.Context, 0, currentIntent, 0);
            this.backgroundUpdateService = AndroidBackgroundServiceFactory.CreateIBackgroundUpdateService2();
        }

        public override void OnReceive(Context context, Intent intent)
        {
            this.backgroundUpdateService.UpdateImagesAsync();
        }

        public void SetAlarm(Context context)
        {
            this.alarmManager.SetExact(AlarmType.Rtc, GetNext8AmTicks(), this.pendingIntent);
        }

        public void SetAlarmNextHour(Context context)
        {
            this.alarmManager.SetExact(AlarmType.Rtc, GetNextHourTicks(), this.pendingIntent);
        }

        public void CancelAlarm(Context context)
        {
            this.alarmManager.Cancel(this.pendingIntent);
        }

        private static long GetNext8AmTicks() => DateTime.Now.GetNextHour(8).Ticks;

        private static long GetNextHourTicks() => DateTime.Now.GetNextHour().Ticks;
    }
}