﻿using Mal.XF.Infra.Fonts;
using Mal.XF.Infra.Navigation;
using Mal.XF.Wallpaper.Localisation;

namespace Mal.XF.Wallpaper.Pages.Main
{
    internal class MainDisplayableToken : IDisplayableNavigationToken
    {
        public INavigationToken NavigationToken { get; } = new MainToken();
        public string Icon => IconFont.Home;
        public string Label => TranslationKeys.Home;
        public int DisplayOrder => 1;
    }
}