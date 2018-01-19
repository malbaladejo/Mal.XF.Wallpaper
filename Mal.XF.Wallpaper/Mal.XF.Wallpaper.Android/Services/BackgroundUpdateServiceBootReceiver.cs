using Android.Content;

namespace Mal.XF.Wallpaper.Droid.Services
{
    internal class BackgroundUpdateServiceBootReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            if (!intent.Action.Equals("android.intent.action.BOOT_COMPLETED"))
                return;

            // Start alarm is needed
        }
    }
}