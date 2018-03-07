using System;
using System.Collections.Generic;
using System.Text;
using Mal.XF.Infra.Log;

namespace Mal.XF.Wallpaper.StateMachines
{
    internal class DeadEndState : IState
    {
        private readonly ILogger logger;

        public DeadEndState(ILogger logger)
        {
            this.logger = logger;
        }

        public void Accept(IStateVisitor visitor)
        {
            this.logger.Debug("Dead end");
            visitor.Visit(this);
        }

        public IState GetNextState() => null;
    }
}
