using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Mal.XF.Infra.Localisation;

namespace Mal.XF.Wallpaper.Localisation
{
    internal class LocalTranslationProvider: LocalTranslationProviderBase
    {
        public LocalTranslationProvider() : base("Mal.XF.Wallpaper.Droid.Localisation.Resources", typeof(LocalTranslationProvider).Assembly)
        {
        }

        internal static string LocalPrefix => "Mal.XF.Wallpaper.Localisation#";
        public override string Prefix => LocalPrefix;
    }
}
