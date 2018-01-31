using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mal.XF.Wallpaper.StateMachines
{
    internal class StateMachine
    {
        private readonly IState initialState;

        public StateMachine(IState initialState)
        {
            this.initialState = initialState;
        }

        public void Execute() => this.ExecuteState(this.initialState);

        private void ExecuteState(IState state)
        {
            state.Execute();

            var newState = state.NextStates.FirstOrDefault(s => s.IsValid());
            if(newState!=null)
                this.ExecuteState(newState);
        }
    }
}
