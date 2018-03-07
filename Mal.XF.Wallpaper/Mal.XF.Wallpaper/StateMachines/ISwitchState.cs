namespace Mal.XF.Wallpaper.StateMachines
{
    internal interface ISwitchState : IState
    {
        void AddNextStates(IState validState, IState invalidState);
    }
}