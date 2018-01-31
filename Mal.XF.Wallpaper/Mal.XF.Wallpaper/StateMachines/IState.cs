using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mal.XF.Wallpaper.StateMachines
{
    internal interface IState
    {
        bool IsValid();

        void Execute();

        IReadOnlyCollection<IState> NextStates { get; }
    }
}
