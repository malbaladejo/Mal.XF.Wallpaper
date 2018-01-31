using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mal.XF.Wallpaper.Services
{
    internal interface IBackgroundUpdateService
    {
        void StartNext8Am();
        void StartNextHour();
        void Stop();
        void StartIfNeeded();
    }
}
