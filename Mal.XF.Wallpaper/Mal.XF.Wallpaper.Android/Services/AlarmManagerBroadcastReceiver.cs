using Android.App;
using Android.Content;
using Mal.XF.Wallpaper.Services;

namespace Mal.XF.Wallpaper.Droid.Services
{
    internal class AlarmManagerBroadcastReceiver : BroadcastReceiver
    {
        private readonly AlarmManager alarmManager;
        private readonly PendingIntent pendingIntent;

        public AlarmManagerBroadcastReceiver()
        {
            var currentIntent = new Intent(Application.Context, typeof(AlarmManagerBroadcastReceiver));
            this.alarmManager = (AlarmManager)Application.Context.GetSystemService(Context.AlarmService);
            this.pendingIntent = PendingIntent.GetBroadcast(Application.Context, 0, currentIntent, 0);
        }

        public async override void OnReceive(Context context, Intent intent)
        {
            //IBingWallpaperService bingWallpaperService;

            //await bingWallpaperService.UpdateImagesIfNeededAsync();
        }

        public void SetAlarm(Context context)
        {
            this.alarmManager.SetInexactRepeating(AlarmType.Rtc, 1, AlarmManager.IntervalHalfDay, this.pendingIntent);
        }
        public void CancelAlarm(Context context)
        {
            this.alarmManager.Cancel(this.pendingIntent);
        }
    }
}