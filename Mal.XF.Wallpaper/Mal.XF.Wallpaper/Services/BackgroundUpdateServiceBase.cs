using System.Collections.Generic;
using System.Threading.Tasks;
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
            this.logger.Info(nameof(StartNext8Am));
            this.Stop();
            this.StartServiceNext8Am();
        }

        public void StartNextHour()
        {
            this.logger.Info(nameof(StartNext8Am));
            this.Stop();
            this.StartServiceNextHour();
        }

        protected abstract void StartServiceNext8Am();
        protected abstract void StartServiceNextHour();

        public abstract void Stop();

        public void StartIfNeeded()
        {
            var settings = this.localStorageService.GetSettings();
            if (!settings.IsUpdateRequired)
                return;

            this.StartNext8Am();
        }
    }
}
