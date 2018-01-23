using Android.Content;
using Mal.XF.Wallpaper.Services;

namespace Mal.XF.Wallpaper.Droid.Services
{
    internal class BackgroundUpdateServiceBootReceiver : BroadcastReceiver
    {
        private readonly IBackgroundUpdateService backgroundUpdateService;

        public BackgroundUpdateServiceBootReceiver()
        {
            this.backgroundUpdateService = BackgroundUpdateServiceFactory.Create();
        }

        public override void OnReceive(Context context, Intent intent)
        {
            if (!intent.Action.Equals("android.intent.action.BOOT_COMPLETED"))
                return;

            this.backgroundUpdateService.StartIfNeeded();
        }
    }
}