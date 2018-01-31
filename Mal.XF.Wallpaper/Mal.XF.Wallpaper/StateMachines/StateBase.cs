using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mal.XF.Wallpaper.StateMachines
{
    internal abstract class StateBase : IState
    {
        private readonly List<IState> nextStates;

        protected StateBase()
        {
            this.nextStates = new List<IState>();
        }

        public abstract bool IsValid();

        public abstract void Execute();

        public void AddState(IState state) => this.nextStates.Add(state);

        public IReadOnlyCollection<IState> NextStates => this.nextStates;
    }
}
