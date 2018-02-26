using Mal.XF.Infra.Extensions;
using Mal.XF.Infra.Log;
using System;

namespace Mal.XF.Wallpaper.Services
{
    internal abstract class BackgroundUpdateServiceBase : IBackgroundUpdateService
    {
        private readonly ILogger logger;

        protected BackgroundUpdateServiceBase(ILogger logger)
        {
            this.logger = logger;
        }

        public void StartNext8Am()
        {
            var nextHour = DateTime.Now.GetNextHour(8);
            this.logger.Info($"{nameof(StartNext8Am)}: {nextHour}");
            this.Stop();
            this.StartService(nextHour);
        }

        public void StartNextHour()
        {
            var nextHour = DateTime.Now.GetCurrentDatePlusOneHour();
            this.logger.Info($"{nameof(StartNextHour)}: {nextHour}");
            this.Stop();
            this.StartService(nextHour);
        }

        protected abstract void StartService(DateTime dateTime);

        public abstract void Stop();
    }
}
