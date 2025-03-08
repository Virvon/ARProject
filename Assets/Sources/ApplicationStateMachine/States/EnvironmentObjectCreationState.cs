using Assets.Sources.BaseLogic.EnvironmentObjectCreation;
using UnityEngine;

namespace Assets.Sources.ApplicationStateMachine.States
{
    public class EnvironmentObjectCreationState : IState
    {
        private readonly EnvironmentObjectCreator _creator;
        private readonly CreatingEnvironmentObjectPositioner _positioner;
        private readonly StateMachine _stateMachine;

        public EnvironmentObjectCreationState(EnvironmentObjectCreator creator, CreatingEnvironmentObjectPositioner positioner, StateMachine stateMachine)
        {
            _creator = creator;
            _positioner = positioner;
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            _creator.Create();

            _positioner.Completed += OnPositionerCompleted;
        }

        public void Exit()
        {
            _positioner.Completed-= OnPositionerCompleted;
            Debug.Log("Change state");
        }

        private void OnPositionerCompleted(EnvironmentObject environmentObject) =>
            _stateMachine.Enter<EnvironmentObjectTransformationState, EnvironmentObject>(environmentObject);
    }
}
