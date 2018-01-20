using System.Threading.Tasks;
using Mal.XF.Wallpaper.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using System;

namespace Mal.XF.Wallpaper.Services
{
    internal class SettingsService : ISettingsService
    {
        private const string SettingsKey = "SettingsKey";
        private const string MetadataKey = "MetadataKey";
        public Settings GetSettings()
        {
            var settingsData = GetProperty<Settings>(SettingsKey);

            if (settingsData != null)
                return settingsData;

            return new Settings
            {
                RefreshScreenLock = RefreshImageType.None,
                RefreshWallpaper = RefreshImageType.None
            };
        }

        public async Task SaveSettingsAsync(Settings settings)
        {
            await SetPropertyAsync(SettingsKey, settings);
        }

        public BingImageMetadata GetMetadata()
        {
            var metadata = GetProperty<BingImageMetadata>(SettingsKey);

            if (metadata != null)
                return metadata;

            return new BingImageMetadata
            {
                UpdateDate = DateTime.MinValue
            };
        }

        public async Task SaveMetadataAsync(BingImageMetadata metadata)
        {
            await SetPropertyAsync(MetadataKey, metadata);
        }

        private static T GetProperty<T>(string key)
        {
            if (Application.Current.Properties.ContainsKey(key))
            {
                var data = (string)Application.Current.Properties[key];
                return JsonConvert.DeserializeObject<T>(data);
            }

            return default(T);
        }

        private static async Task SetPropertyAsync(string key, object value)
        {
            Application.Current.Properties[key] = JsonConvert.SerializeObject(value);
            await Application.Current.SavePropertiesAsync();
        }
    }
}