using Android.Content;
using Mal.XF.Infra.Log;
using Mal.XF.Wallpaper.StateMachines;
using System;

namespace Mal.XF.Wallpaper.Droid.Services
{
    [BroadcastReceiver(Name = "wallpaper.alarmmanagerbroadcastreceiver")]
    public class AlarmManagerBroadcastReceiver : BroadcastReceiver
    {
        private readonly ILogger logger;

        public AlarmManagerBroadcastReceiver()
        {
            this.logger = AndroidBackgroundServiceFactory.CreateLogger();
            this.logger.Info($"{nameof(AndroidBackgroundServiceFactory)}.ctor");
        }

        public override void OnReceive(Context context, Intent intent)
        {
            try
            {
                this.logger.Info($"{nameof(BootCompletedBroadcastReceiver)}.{nameof(this.OnReceive)}");
                var stateFactory = AndroidBackgroundServiceFactory.CreateStateFactory();
                var stateMachine = new StateMachine(stateFactory.GetInitialStateForDeamon(), this.logger);
                stateMachine.Execute();
            }
            catch (Exception e)
            {
                this.logger.Error($"{nameof(BootCompletedBroadcastReceiver)}.{nameof(this.OnReceive)}", e);
            }
        }
    }
}