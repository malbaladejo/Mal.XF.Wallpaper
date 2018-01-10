using Mal.XF.Infra.Navigation;

namespace Mal.XF.Wallpaper.Pages.Configuration
{
    internal class ConfigurationDisplayableToken : IDisplayableNavigationToken
    {
        public INavigationToken NavigationToken { get; } = new ConfigurationToken();
        public string Icon => "C";
        public string Label => "Configuration";
    }
}