using System;
using System.Collections.Generic;
using System.Text;
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

        public StateFactory(ILocalStorageService localStorageService, 
                            INetworkService networkService, 
                            IWallpaperBackgroundService wallpaperBackgroundService,
                            IBingWallpaperRepository bingWallpaperService,
                            IBackgroundUpdateService backgroundUpdateService)
        {
            this.localStorageService = localStorageService;
            this.networkService = networkService;
            this.wallpaperBackgroundService = wallpaperBackgroundService;
            this.bingWallpaperService = bingWallpaperService;
            this.backgroundUpdateService = backgroundUpdateService;
        }
        public IState GetInitialState()
        {
            var initialState = new DefaultState();
            var isUpdateRequiredBaseOnSettingsState = new IsUpdateRequiredBaseOnSettingsState(this.localStorageService);
            var isWifiEnabledState = new IsWifiEnabledState(this.networkService);
            var isWifiRequiredState = new IsWifiRequiredState(this.localStorageService);
            var isUpdateRequiredBaseOnLastUpdateState = new IsUpdateRequiredBaseOnLastUpdateState(this.localStorageService);
            var updateImagesState = new UpdateImagesState(this.wallpaperBackgroundService);
            var setLastUpdateState = new SetLastUpdateState(this.localStorageService);
            var isNewImagesAvailableState = new IsNewImagesAvailableState(this.bingWallpaperService, this.localStorageService);
            var scheduleBackgroundUpdateServiceToNextHourState = new ScheduleBackgroundUpdateServiceToNextHourState(this.backgroundUpdateService);
            var scheduleBackgroundUpdateServiceTo8AmState = new ScheduleBackgroundUpdateServiceTo8AmState(this.backgroundUpdateService);

            initialState.AddState(isUpdateRequiredBaseOnSettingsState);

            isUpdateRequiredBaseOnSettingsState.AddState(isUpdateRequiredBaseOnLastUpdateState);

            isUpdateRequiredBaseOnSettingsState.AddState(isWifiRequiredState);
            isUpdateRequiredBaseOnSettingsState.AddState(isNewImagesAvailableState);

            isWifiRequiredState.AddState(isWifiEnabledState);
            isWifiRequiredState.AddState(scheduleBackgroundUpdateServiceToNextHourState);

            isWifiEnabledState.AddState(isNewImagesAvailableState);
            isWifiEnabledState.AddState(scheduleBackgroundUpdateServiceToNextHourState);

            isNewImagesAvailableState.AddState(updateImagesState);

            updateImagesState.AddState(setLastUpdateState);

            setLastUpdateState.AddState(scheduleBackgroundUpdateServiceTo8AmState);

            return initialState;
        }
    }
}
