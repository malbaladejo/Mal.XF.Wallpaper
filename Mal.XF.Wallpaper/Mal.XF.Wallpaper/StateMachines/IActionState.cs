namespace Mal.XF.Wallpaper.StateMachines
{
    internal interface IActionState : IState
    {
        void AddNextStates(IState nextState, IState errorState);
        void Execute();
    }
}