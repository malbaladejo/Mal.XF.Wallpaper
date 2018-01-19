using System;
using System.Collections.Generic;
using System.Text;

namespace Mal.XF.Wallpaper.Services
{
    internal interface IBackgroundUpdateService
    {
        void Start();
        void Stop();
        void StartIfNeeded();
    }
}
