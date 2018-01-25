using System.Collections.Generic;
using System.Threading.Tasks;
using Mal.XF.Wallpaper.Models;

namespace Mal.XF.Wallpaper.Services
{
    internal abstract class BackgroundUpdateServiceBase : IBackgroundUpdateService
    {
        private readonly ILocalStorageService localStorageService;

        public BackgroundUpdateServiceBase(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }

        public void Start()
        {
            this.Stop();
            this.StartService();
        }

        protected abstract void StartService();

        public abstract void Stop();

        public void StartIfNeeded()
        {
            var settings = this.localStorageService.GetSettings();
            if (!settings.IsUpdateRequired)
                return;

            this.Start();
        }
    }
}
