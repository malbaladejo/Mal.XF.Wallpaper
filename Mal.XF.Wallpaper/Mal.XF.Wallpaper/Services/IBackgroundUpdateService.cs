using System;
using System.Collections.Generic;
using System.Text;

namespace Mal.XF.Wallpaper.Services
{
    internal interface IBackgroundUpdateService
    {
        void Start();
        void Stop();
        void StartIfNeeded();
    }

    internal abstract class BackgroundUpdateServiceBase : IBackgroundUpdateService
    {
        private readonly ISettingsService settingsService;

        public BackgroundUpdateServiceBase(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        public void Start()
        {
            this.Stop();

        }

        public abstract void Stop();

        public void StartIfNeeded()
        {
            var settings = this.settingsService.GetSettings();
            if (!settings.IsUpdateRequired)
                return;

            this.Start();
        }
    }
}
