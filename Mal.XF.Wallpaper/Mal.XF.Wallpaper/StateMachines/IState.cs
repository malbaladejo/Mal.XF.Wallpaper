using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mal.XF.Wallpaper.StateMachines
{
    internal interface IState
    {
        void Accept(IStateVisitor visitor);

        IState GetNextState();
    }
}
