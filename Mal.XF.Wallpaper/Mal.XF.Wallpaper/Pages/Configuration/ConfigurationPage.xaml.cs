using System;
using Xamarin.Forms;

namespace Mal.XF.Wallpaper.Pages.Configuration
{
    public partial class ConfigurationPage : ContentPage
    {
        public ConfigurationPage()
        {
            try
            {
                InitializeComponent();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
