namespace Assets.Sources.ApplicationStateMachine
{
    public interface IPayloadState<TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }
}
