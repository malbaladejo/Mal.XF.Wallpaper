using Mal.XF.Infra;
using Microsoft.Practices.Unity;

namespace Mal.XF.Wallpaper.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            var app = new Mal.XF.Wallpaper.App(new UwpInitializer());
            LoadApplication(app);
        }
    }

    internal class UwpInitializer : UwpInitializerBase
    {
        public override void RegisterTypes(IUnityContainer container)
        {
            base.RegisterTypes(container);
        }
    }
}
