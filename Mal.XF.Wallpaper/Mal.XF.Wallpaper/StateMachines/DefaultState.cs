using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mal.XF.Wallpaper.StateMachines
{
    internal class DefaultState : StateBase
    {
        public override bool IsValid() => true;

        public override void Execute()
        {
            // Nothing to do
        }
    }
}
