using Mal.XF.Infra.Navigation;
using Mal.XF.Wallpaper.Localisation;

namespace Mal.XF.Wallpaper.Pages.Configuration
{
    internal class ConfigurationDisplayableToken : IDisplayableNavigationToken
    {
        public INavigationToken NavigationToken { get; } = new ConfigurationToken();
        public string Icon => "#";
        public string Label => TranslationKeys.Settings;
        public int DisplayOrder => 2;
    }
}