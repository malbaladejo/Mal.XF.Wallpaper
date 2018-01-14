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
}
