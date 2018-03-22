using Mal.XF.Infra.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mal.XF.Wallpaper.StateMachines
{
    internal class StateMachine : IStateVisitor
    {
        private readonly IState initialState;
        private readonly ILogger logger;

        public StateMachine(IState initialState, ILogger logger)
        {
            this.initialState = initialState;
            this.logger = logger;
        }

        public void Execute()
        {
            this.logger.Debug($"Start State manchine.");
            this.initialState.Accept(this);
            this.logger.Debug($"Stop State manchine.");
        }

        public void Visit(ISwitchState state)
        {
            try
            {
                this.logger.Debug($"Switch on {state.GetType().Name}.");
                this.Visit((IState)state);
            }
            catch (Exception e)
            {
                this.logger.Error($"Error in {state.GetType().Name}.", e);
            }
        }

        public void Visit(IActionState state)
        {
            try
            {
                this.logger.Debug($"{state.GetType().Name} executing.");
                state.Execute();
                this.logger.Debug($"{state.GetType().Name} executed.");
                this.Visit((IState)state);
            }
            catch (Exception e)
            {
                this.logger.Error($"Error in {state.GetType().Name}.", e);
            }
        }

        public void Visit(DeadEndState state)
        {
            this.logger.Debug("Dead end");
        }

        private void Visit(IState state)=> state?.GetNextState()?.Accept(this);
    }
}
