using Assets.Sources.ApplicationStateMachine;
using Assets.Sources.ApplicationStateMachine.States;
using Assets.Sources.BaseLogic.EnvironmentObjectTransformation;
using System;

namespace Assets.Sources.BaseLogic.EnvironmentObjectCreation
{
    public class CreationModel : IDisposable
    {
        private readonly StateMachine _stateMachine;
        private readonly EnvironmentObjectCreator _creator;
        private readonly EnvironmentObjectCameraPositioner _positioner;
        private readonly EnvironmentObjectTransformator _transformator;

        public CreationModel(StateMachine stateMachine, EnvironmentObjectCreator creator, EnvironmentObjectCameraPositioner positioner, EnvironmentObjectTransformator transformator)
        {
            _stateMachine = stateMachine;
            _creator = creator;
            _positioner = positioner;
            _transformator = transformator;

            _positioner.Completed += OnPositionerCompleted;
            _creator.Created += OnCreated;
        }

        public void Dispose()
        {
            _positioner.Completed -= OnPositionerCompleted;
            _creator.Created -= OnCreated;
        }

        public void MoveReviewState() =>
            _stateMachine.Enter<ReviewState>();

        public void ChangeObject(bool isNextObject) =>
            _creator.ChangeObject(isNextObject);

        private void OnPositionerCompleted() =>
            _stateMachine.Enter<EnvironmentObjectTransformationState, EnvironmentObject.EnvironmentObject>(_creator.CurrentObject);

        private void OnCreated() =>
            _transformator.SetObject(_creator.CurrentObject);        
    }
}
