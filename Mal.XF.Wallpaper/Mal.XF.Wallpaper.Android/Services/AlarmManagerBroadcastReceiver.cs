using Android.Content;
using Mal.XF.Infra.Log;
using Mal.XF.Wallpaper.StateMachines;
using System;

namespace Mal.XF.Wallpaper.Droid.Services
{
    [BroadcastReceiver]
    internal class AlarmManagerBroadcastReceiver : BroadcastReceiver
    {
        private readonly ILogger logger;
        private readonly StateFactory stateFactory;

        public AlarmManagerBroadcastReceiver()
        {
            this.logger = AndroidBackgroundServiceFactory.CreateLogger();
            this.stateFactory = AndroidBackgroundServiceFactory.CreateStateFactory();
        }

        public override void OnReceive(Context context, Intent intent)
        {
            try
            {
                this.logger.Info($"{nameof(BootCompletedBroadcastReceiver)}.{nameof(this.OnReceive)}");
                var stateMachine = new StateMachine(this.stateFactory.GetInitialStateForDeamon(), this.logger);
                stateMachine.Execute();
            }
            catch (Exception e)
            {
                this.logger.Error($"{nameof(BootCompletedBroadcastReceiver)}.{nameof(this.OnReceive)}", e);
            }
        }
    }
}