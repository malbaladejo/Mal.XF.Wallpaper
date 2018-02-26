using Android.App;
using Android.Content;
using Mal.XF.Infra.Log;
using System;

namespace Mal.XF.Wallpaper.Droid.Services
{
    internal class AlarmManagerFacade
    {
        private readonly AlarmManager alarmManager;
        private readonly PendingIntent pendingIntent;
        private readonly ILogger logger;

        public AlarmManagerFacade(ILogger logger)
        {
            this.logger = logger;
            var currentIntent = new Intent(Application.Context, typeof(AlarmManagerBroadcastReceiver));
            this.alarmManager = (AlarmManager)Application.Context.GetSystemService(Context.AlarmService);
            this.pendingIntent = PendingIntent.GetBroadcast(Application.Context, 0, currentIntent, 0);
        }

        public void SetAlarm(Context context, DateTime dateTime)
        {
            try
            {
                this.logger.Info($"{nameof(this.SetAlarm)}: {dateTime}");
                this.alarmManager.SetExact(AlarmType.Rtc, dateTime.Ticks, this.pendingIntent);
            }
            catch (Exception e)
            {
                this.logger.Error($"Error during {nameof(this.SetAlarm)}", e);
            }
        }

        public void CancelAlarm(Context context)
        {
            try
            {
                this.logger.Info(nameof(this.CancelAlarm));
                this.alarmManager.Cancel(this.pendingIntent);
            }
            catch (Exception e)
            {
                this.logger.Error($"Error during  {nameof(this.CancelAlarm)}", e);
            }
        }
    }

}