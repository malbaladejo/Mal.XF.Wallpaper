using System;

namespace Mal.XF.Wallpaper.StateMachines
{
    internal abstract class SwitchStateBase : ISwitchState
    {
        private IState validState;
        private IState invalidState;

        public void Accept(IStateVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public IState GetNextState()
        {
            if (this.IsValid())
            {
                if (this.validState == null)
                    throw new InvalidOperationException($"{nameof(validState)} must be initialized.");
                return
                    this.validState;
            }

            if (this.invalidState == null)
                throw new InvalidOperationException($"{nameof(invalidState)} must be initialized.");
            return this.invalidState;
        }

        public void AddNextStates(IState newValidState, IState newInvalidState)
        {
            this.validState = newValidState;
            this.invalidState = newInvalidState;
        }

        protected abstract bool IsValid();
    }
}