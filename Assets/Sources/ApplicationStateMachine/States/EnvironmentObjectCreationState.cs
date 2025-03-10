using Assets.Sources.BaseLogic.EnvironmentObjectCreation;
using Assets.Sources.BaseLogic.EnvironmentObjectTransformation;
using UnityEngine;

namespace Assets.Sources.ApplicationStateMachine.States
{
    public class EnvironmentObjectCreationState : IState
    {
        private readonly EnvironmentObjectCreator _creator;
        private readonly EnvironmentObjectCameraPositioner _positioner;
        private readonly StateMachine _stateMachine;
        private readonly EnvironmentObjectTransformator _transformator;

        public EnvironmentObjectCreationState(
            EnvironmentObjectCreator creator,
            EnvironmentObjectCameraPositioner positioner,
            StateMachine stateMachine,
            EnvironmentObjectTransformator transformator)
        {
            _creator = creator;
            _positioner = positioner;
            _stateMachine = stateMachine;
            _transformator = transformator;
        }

        public void Enter()
        {
            _creator.Create();
            _positioner.SetActive(true);
            _transformator.SetActive(true);

            _positioner.Completed += OnPositionerCompleted;
        }

        public void Exit()
        {
            _positioner.SetActive(false);
            _transformator.SetActive(false);

            _positioner.Completed -= OnPositionerCompleted;
            Debug.Log("Change state");
        }

        private void OnPositionerCompleted() =>
            _stateMachine.Enter<EnvironmentObjectTransformationState>();
    }
}
