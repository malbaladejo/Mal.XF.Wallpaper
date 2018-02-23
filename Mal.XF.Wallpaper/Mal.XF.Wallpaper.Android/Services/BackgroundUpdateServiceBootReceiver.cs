using Android.Content;
using Mal.XF.Infra.Log;
using Mal.XF.Wallpaper.Services;
using System;

namespace Mal.XF.Wallpaper.Droid.Services
{
    internal class BackgroundUpdateServiceBootReceiver : BroadcastReceiver
    {
        private readonly IBackgroundUpdateService backgroundUpdateService;
        private readonly ILogger logger;

        public BackgroundUpdateServiceBootReceiver()
        {
            this.backgroundUpdateService = AndroidBackgroundServiceFactory.CreateBackgroundUpdateService();
            this.logger = AndroidBackgroundServiceFactory.CreateLogger();
        }

        public override void OnReceive(Context context, Intent intent)
        {
            if (!intent.Action.Equals("android.intent.action.BOOT_COMPLETED"))
                return;

            try
            {
                this.logger.Info($"{nameof(BackgroundUpdateServiceBootReceiver)}.{nameof(this.OnReceive)}");
                this.backgroundUpdateService.StartIfNeeded();
            }
            catch (Exception e)
            {
                this.logger.Error($"{nameof(BackgroundUpdateServiceBootReceiver)}.{nameof(this.OnReceive)}", e);
            }
        }
    }
}