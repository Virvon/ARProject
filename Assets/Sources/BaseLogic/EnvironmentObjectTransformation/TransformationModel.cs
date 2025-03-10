using Assets.Sources.ApplicationStateMachine;
using Assets.Sources.ApplicationStateMachine.States;

namespace Assets.Sources.BaseLogic.EnvironmentObjectTransformation
{
    public class TransformationModel
    {
        private readonly StateMachine _stateMachine;

        public TransformationModel(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void MoveNextState() =>
            _stateMachine.Enter<ReviewState>();
    }
}
