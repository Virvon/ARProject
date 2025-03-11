using Assets.Sources.ApplicationStateMachine;
using Assets.Sources.ApplicationStateMachine.States;

namespace Assets.Sources.BaseLogic.Information
{
    public class InformationModel
    {
        private readonly StateMachine _stateMachine;

        public InformationModel(StateMachine stateMachine) =>
            _stateMachine = stateMachine;

        public void MoveNextState() =>
            _stateMachine.Enter<ReviewState>();
    }
}
