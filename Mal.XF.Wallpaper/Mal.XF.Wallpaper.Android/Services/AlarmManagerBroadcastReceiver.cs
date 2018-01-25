using Android.App;
using Android.Content;
using Android.Icu.Util;
using Java.Lang;
using Mal.XF.Wallpaper.Services;
using TimeZone = Android.Icu.Util.TimeZone;

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
            this.alarmManager.SetInexactRepeating(AlarmType.Rtc, this.GetTriggerAtMillis(), AlarmManager.IntervalHalfDay, this.pendingIntent);
        }

        public void CancelAlarm(Context context)
        {
            this.alarmManager.Cancel(this.pendingIntent);
        }

        private long GetTriggerAtMillis()
        {
            var calendar = Calendar.GetInstance(TimeZone.Default);
            calendar.TimeInMillis = JavaSystem.CurrentTimeMillis();
            calendar.Set(CalendarField.HourOfDay, 9);
            return calendar.TimeInMillis;
        }
    }
}