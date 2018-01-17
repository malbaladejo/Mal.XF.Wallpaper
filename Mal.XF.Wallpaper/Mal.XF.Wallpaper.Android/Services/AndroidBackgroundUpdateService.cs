

using Android.App;
using Android.Content;

namespace Mal.XF.Wallpaper.Droid.Services
{
    internal class AndroidBackgroundUpdateService
    {
        private readonly AlarmManager alarmManager;
        private PendingIntent updateIntent;

        public AndroidBackgroundUpdateService()
        {
            this.alarmManager = (AlarmManager)Application.Context.GetSystemService(Context.AlarmService);
        }

        public void Start()
        {
            this.alarmManager.SetInexactRepeating(AlarmType.Rtc, 1, AlarmManager.IntervalHalfDay, this.updateIntent);
        }

        public void Stop()
        {
            this.alarmManager.Cancel(this.updateIntent);
        }
    }

    internal class BackgroundUpdateServiceBootReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.Action.Equals("android.intent.action.BOOT_COMPLETED"))
            {
                // Set the alarm here.
            }
        }
    }
}