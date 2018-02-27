using Android.Content;
using Mal.XF.Infra.Log;
using Mal.XF.Wallpaper.StateMachines;
using System;

namespace Mal.XF.Wallpaper.Droid.Services
{
    [BroadcastReceiver(Name = "wallpaper.broadcastreceiver")]
    public class BootCompletedBroadcastReceiver : BroadcastReceiver
    {
        private readonly ILogger logger;

        public BootCompletedBroadcastReceiver()
        {
            this.logger = AndroidBackgroundServiceFactory.CreateLogger();
        }

        public override void OnReceive(Context context, Intent intent)
        {
            if (!intent.Action.Equals("android.intent.action.BOOT_COMPLETED"))
                return;

            try
            {
                this.logger.Info($"{nameof(BootCompletedBroadcastReceiver)}.{nameof(this.OnReceive)}");
                var stateFactory = AndroidBackgroundServiceFactory.CreateStateFactory();
                var stateMachine = new StateMachine(stateFactory.GetInitialStateForDeviceBoot(), this.logger);
                stateMachine.Execute();
            }
            catch (Exception e)
            {
                this.logger.Error($"{nameof(BootCompletedBroadcastReceiver)}.{nameof(this.OnReceive)}", e);
            }
        }
    }
}