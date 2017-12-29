using Mal.XF.Infra.Navigation;

namespace Mal.XF.Wallpaper.Pages.Main
{
    internal class MainDisplayableToken : IDisplayableNavigationToken
    {
        public INavigationToken NavigationToken { get; } = new MainToken();
        public string Icon => "M";
        public string Label => "Home";
    }
}