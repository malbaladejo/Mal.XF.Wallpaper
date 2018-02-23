using Android.App;
using Android.Content;
using Mal.XF.Infra.Log;
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
        private readonly ILogger logger;

        public AlarmManagerBroadcastReceiver()
        {
            var currentIntent = new Intent(Application.Context, typeof(AlarmManagerBroadcastReceiver));
            this.alarmManager = (AlarmManager)Application.Context.GetSystemService(Context.AlarmService);
            this.pendingIntent = PendingIntent.GetBroadcast(Application.Context, 0, currentIntent, 0);
            this.backgroundUpdateService = AndroidBackgroundServiceFactory.CreateIBackgroundUpdateService2();
            this.logger = AndroidBackgroundServiceFactory.CreateLogger();
        }

        public override void OnReceive(Context context, Intent intent)
        {
            try
            {
                this.logger.Info($"{nameof(AlarmManagerBroadcastReceiver)}.{nameof(this.OnReceive)}");
                this.backgroundUpdateService.UpdateImagesAsync();
            }
            catch (Exception e)
            {
                this.logger.Error($"{nameof(AlarmManagerBroadcastReceiver)}.{nameof(this.OnReceive)}", e);
            }
        }

        public void SetAlarm(Context context, DateTime dateTime)
        {
            try
            {
                this.logger.Info($"{nameof(AlarmManagerBroadcastReceiver)}.{nameof(this.SetAlarm)}");
                this.alarmManager.SetExact(AlarmType.Rtc, dateTime.Ticks, this.pendingIntent);
            }
            catch (Exception e)
            {
                this.logger.Error($"{nameof(AlarmManagerBroadcastReceiver)}.{nameof(this.SetAlarm)}", e);
            }
        }

        public void CancelAlarm(Context context)
        {
            try
            {
                this.logger.Info($"{nameof(AlarmManagerBroadcastReceiver)}.{nameof(this.CancelAlarm)}");
                this.alarmManager.Cancel(this.pendingIntent);
            }
            catch (Exception e)
            {
                this.logger.Error($"{nameof(AlarmManagerBroadcastReceiver)}.{nameof(this.CancelAlarm)}", e);
            }
        }
    }
}