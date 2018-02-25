using Mal.XF.Infra.Log;
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
        private readonly ILogger logger;

        public StateMachine(IState initialState, ILogger logger)
        {
            this.initialState = initialState;
            this.logger = logger;
        }

        public void Execute() => this.ExecuteState(this.initialState);

        private void ExecuteState(IState state)
        {
            try
            {
                this.logger.Debug($"{state.GetType().Name} executing.");
                state.Execute();
                this.logger.Debug($"{state.GetType().Name} executed.");

                var newState = state.NextStates.FirstOrDefault(s => s.IsValid());
                if (newState != null)
                    this.ExecuteState(newState);
            }
            catch (Exception e)
            {
                this.logger.Error($"Error in {state.GetType().Name}.", e);
            }
        }
    }
}
