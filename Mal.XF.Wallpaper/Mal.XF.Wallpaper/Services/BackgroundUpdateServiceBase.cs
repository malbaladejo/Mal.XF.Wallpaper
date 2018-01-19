namespace Mal.XF.Wallpaper.Services
{
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
            this.StartService();
        }

        protected abstract void StartService();

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
