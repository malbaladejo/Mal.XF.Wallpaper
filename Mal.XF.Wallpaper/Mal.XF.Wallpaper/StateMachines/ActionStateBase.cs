using System;

namespace Mal.XF.Wallpaper.StateMachines
{
    internal abstract class ActionStateBase : IActionState
    {
        private IState nextState;
        private IState errorState;
        private bool isValid;

        public void Accept(IStateVisitor visitor)
        {
            visitor.Visit(this);
        }

        public IState GetNextState()
        {
            if (this.isValid)
            {
                if (this.nextState == null)
                    throw new InvalidOperationException($"{nameof(nextState)} must be initialized.");
                return
                    this.nextState;
            }

            if (this.errorState == null)
                throw new InvalidOperationException($"{nameof(errorState)} must be initialized.");
            return this.errorState;
        }

        public void AddNextStates(IState newNextState, IState newErrorState)
        {
            this.nextState = newNextState;
            this.errorState = newErrorState;
        }

        public void Execute()
        {
            try
            {
                this.DoAction();
                this.isValid = true;
            }
            catch
            {
                this.isValid = false;
            }
        }

        protected abstract void DoAction();
    }
}