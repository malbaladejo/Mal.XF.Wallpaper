namespace Mal.XF.Wallpaper.StateMachines
{
    internal interface IStateVisitor
    {
        void Visit(ISwitchState state);
        void Visit(IActionState state);
        void Visit(DeadEndState state);
    }
}