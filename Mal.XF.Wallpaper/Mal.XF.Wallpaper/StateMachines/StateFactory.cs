using Mal.XF.Infra.Log;
using Mal.XF.Infra.Net;
using Mal.XF.Wallpaper.Services;

namespace Mal.XF.Wallpaper.StateMachines
{
    internal class StateFactory
    {
        private readonly ILocalStorageService localStorageService;
        private readonly INetworkService networkService;
        private readonly IWallpaperBackgroundService wallpaperBackgroundService;
        private readonly IBingWallpaperService bingWallpaperService;
        private readonly IBackgroundUpdateService backgroundUpdateService;
        private readonly ILogger logger;

        public StateFactory(ILocalStorageService localStorageService,
                            INetworkService networkService,
                            IWallpaperBackgroundService wallpaperBackgroundService,
                            IBingWallpaperRepository bingWallpaperService,
                            IBackgroundUpdateService backgroundUpdateService,
                            ILogger logger)
        {
            this.localStorageService = localStorageService;
            this.networkService = networkService;
            this.wallpaperBackgroundService = wallpaperBackgroundService;
            this.bingWallpaperService = bingWallpaperService;
            this.backgroundUpdateService = backgroundUpdateService;
            this.logger = logger;
        }

        /// <summary>
        /// Donne l'état initial de la machine état à exécuter lors du démarrage du device.
        /// </summary>
        public IState GetInitialStateForDeviceBoot()
        {
            var deadEndState = new DeadEndState(this.logger);
            var isUpdateRequiredBaseOnSettingsState = new IsUpdateRequiredBaseOnSettingsState(this.localStorageService, this.logger);
            var isCurrentHourBefore8AmState = new IsCurrentHourBefore8AmState(this.logger);
            var scheduleBackgroundUpdateServiceToNextHourState = new ScheduleBackgroundUpdateServiceToNextHourState(this.backgroundUpdateService, this.logger);
            var scheduleBackgroundUpdateServiceTo8AmState = new ScheduleBackgroundUpdateServiceTo8AmState(this.backgroundUpdateService, this.logger);


            isUpdateRequiredBaseOnSettingsState.AddNextStates(isCurrentHourBefore8AmState, deadEndState);
            isCurrentHourBefore8AmState.AddNextStates(scheduleBackgroundUpdateServiceTo8AmState, scheduleBackgroundUpdateServiceToNextHourState);
            scheduleBackgroundUpdateServiceToNextHourState.AddNextStates(deadEndState, deadEndState);
            return isUpdateRequiredBaseOnSettingsState;
        }

        /// <summary>
        /// Donne l'état initial de la machine état à exécuter lorsque le démon se lance.
        /// </summary>
        public IState GetInitialStateForDeamon()
        {
            var deadEndState = new DeadEndState(this.logger);
            var isUpdateRequiredBaseOnSettingsState = new IsUpdateRequiredBaseOnSettingsState(this.localStorageService, this.logger);
            var isWifiEnabledState = new IsWifiEnabledState(this.networkService, this.logger);
            var isWifiRequiredState = new IsWifiRequiredState(this.localStorageService, this.logger);
            var isUpdateRequiredBaseOnLastUpdateState = new IsUpdateRequiredBaseOnLastUpdateState(this.localStorageService, this.logger);
            var updateImagesState = new UpdateImagesState(this.wallpaperBackgroundService, this.logger);
            var setLastUpdateState = new SetLastUpdateState(this.localStorageService, this.logger);
            var isNewImagesAvailableState = new IsNewImagesAvailableState(this.bingWallpaperService, this.localStorageService, this.logger);
            var scheduleBackgroundUpdateServiceToNextHourState = new ScheduleBackgroundUpdateServiceToNextHourState(this.backgroundUpdateService, this.logger);
            var scheduleBackgroundUpdateServiceTo8AmState = new ScheduleBackgroundUpdateServiceTo8AmState(this.backgroundUpdateService, this.logger);

            isUpdateRequiredBaseOnSettingsState.AddNextStates(isUpdateRequiredBaseOnLastUpdateState, deadEndState);

            isUpdateRequiredBaseOnLastUpdateState.AddNextStates(isWifiRequiredState, scheduleBackgroundUpdateServiceTo8AmState);

            isWifiRequiredState.AddNextStates(isWifiEnabledState, isNewImagesAvailableState);

            isWifiEnabledState.AddNextStates(isNewImagesAvailableState, scheduleBackgroundUpdateServiceToNextHourState);

            isNewImagesAvailableState.AddNextStates(updateImagesState, scheduleBackgroundUpdateServiceToNextHourState);

            updateImagesState.AddNextStates(setLastUpdateState, scheduleBackgroundUpdateServiceToNextHourState);

            setLastUpdateState.AddNextStates(scheduleBackgroundUpdateServiceTo8AmState, deadEndState);

            scheduleBackgroundUpdateServiceToNextHourState.AddNextStates(deadEndState, deadEndState);

            scheduleBackgroundUpdateServiceTo8AmState.AddNextStates(deadEndState, deadEndState);

            return isUpdateRequiredBaseOnSettingsState;
        }
    }
}
