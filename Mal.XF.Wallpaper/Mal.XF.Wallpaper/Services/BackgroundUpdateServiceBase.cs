using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mal.XF.Infra.Extensions;
using Mal.XF.Infra.Log;
using Mal.XF.Wallpaper.Models;

namespace Mal.XF.Wallpaper.Services
{
    internal abstract class BackgroundUpdateServiceBase : IBackgroundUpdateService
    {
        private readonly ILocalStorageService localStorageService;
        private readonly ILogger logger;

        public BackgroundUpdateServiceBase(ILocalStorageService localStorageService, ILogger logger)
        {
            this.localStorageService = localStorageService;
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
            var nextHour = DateTime.Now.GetNextHour();
            this.logger.Info($"{nameof(StartNextHour)}: {nextHour}");
            this.Stop();
            this.StartService(nextHour);
        }

        protected abstract void StartService(DateTime dateTime);

        public abstract void Stop();

        public void StartIfNeeded()
        {
            this.logger.Debug($"{nameof(BackgroundUpdateServiceBase)}.{nameof(this.StartIfNeeded)}");

            var settings = this.localStorageService.GetSettings();
            if (!settings.IsUpdateRequired)
                return;
            this.logger.Debug($"{nameof(BackgroundUpdateServiceBase)}.{nameof(this.StartIfNeeded)}: {nameof(settings.IsUpdateRequired)}: {settings.IsUpdateRequired}");
            this.StartNext8Am();
        }
    }
}
