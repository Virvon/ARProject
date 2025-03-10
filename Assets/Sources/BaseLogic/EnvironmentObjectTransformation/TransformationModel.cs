using Assets.Sources.ApplicationStateMachine;
using Assets.Sources.ApplicationStateMachine.States;
using System;

namespace Assets.Sources.BaseLogic.EnvironmentObjectTransformation
{
    public class TransformationModel
    {
        private readonly StateMachine _stateMachine;
        private readonly EnvironmentObject.EnvironmentObject _environmentObject;

        public TransformationModel(StateMachine stateMachine, EnvironmentObject.EnvironmentObject environmentObject)
        {
            _stateMachine = stateMachine;
            _environmentObject = environmentObject;
        }

        public void MoveReviewState() =>
            _stateMachine.Enter<ReviewState>();

        public void MoveColorSelectionState() =>
            _stateMachine.Enter<ColorSelectionState, EnvironmentObject.EnvironmentObject>(_environmentObject);

        public void DestroyEnvironmentObject()
        {
            _environmentObject.Destroy();
            _stateMachine.Enter<ReviewState>();
        }
    }
}
