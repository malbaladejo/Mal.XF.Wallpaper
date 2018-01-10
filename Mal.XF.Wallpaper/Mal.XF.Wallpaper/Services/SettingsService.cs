using System.Threading.Tasks;
using Mal.XF.Wallpaper.Models;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Mal.XF.Wallpaper.Services
{
    internal class SettingsService : ISettingsService
    {
        private const string SettingsKey = "SettingsKey";
        public Settings GetSettings()
        {
            var settingsData = GetProperty<string>(SettingsKey);

            if (string.IsNullOrEmpty(settingsData))
                return new Settings
                {
                    RefreshScreenLock = RefreshImageType.None,
                    RefreshWallpaper = RefreshImageType.None
                };

            return JsonConvert.DeserializeObject<Settings>(settingsData);
        }

        public async Task SaveSettingsAsync(Settings settings)
        {
            var settingsData = JsonConvert.SerializeObject(settings);
            await GetPropertyAsync(SettingsKey, settingsData);
        }

        private static T GetProperty<T>(string key)
        {
            if (Application.Current.Properties.ContainsKey(key))
                return (T)Application.Current.Properties[key];

            return default(T);
        }

        private static async Task GetPropertyAsync(string key, object value)
        {
            Application.Current.Properties[key] = value;
            await Application.Current.SavePropertiesAsync();
        }
    }
}