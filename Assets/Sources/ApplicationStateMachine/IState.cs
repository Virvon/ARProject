namespace Assets.Sources.ApplicationStateMachine
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}
