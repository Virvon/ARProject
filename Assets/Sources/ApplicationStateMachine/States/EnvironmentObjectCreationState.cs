using Assets.Sources.BaseLogic.EnvironmentObject;
using Assets.Sources.BaseLogic.EnvironmentObjectCreation;
using Assets.Sources.BaseLogic.EnvironmentObjectTransformation;
using Assets.Sources.Services.InputService;
using Assets.Sources.Services.TickService;
using Assets.Sources.StaticDataService;
using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Assets.Sources.ApplicationStateMachine.States
{
    public class EnvironmentObjectCreationState : IState
    {
        private readonly IStaticDataService _staticDataService;
        private readonly ARRaycastManager _raycastManager;
        private readonly Camera _camera;
        private readonly IInputService _inputService;
        private readonly CreationView _creationView;
        private readonly StateMachine _stateMachine;
        private readonly TickService _tickService;

        EnvironmentObjectCameraPositioner _positioner;
        CreatorPresenter _creatorPresenter;
        EnvironmentObjectCreator _creator;
        EnvironmentObjectTransformator _transformator;

        public EnvironmentObjectCreationState(
            IStaticDataService staticDataService,
            ARRaycastManager raycastManager,
            Camera camera,
            IInputService inputService,
            CreationView creationView,
            StateMachine stateMachine,
            TickService tickService)
        {
            _staticDataService = staticDataService;
            _raycastManager = raycastManager;
            _camera = camera;
            _inputService = inputService;
            _creationView = creationView;
            _stateMachine = stateMachine;
            _tickService = tickService;
        }

        public void Enter()
        {
            Debug.Log("Enter to creation state");

            _creator = new(_staticDataService);
            _positioner = new(_creator, _raycastManager, _camera, _inputService);
            _creatorPresenter = new(_creationView, _creator);
            _transformator = new(_inputService, _camera);

            _creationView.Show();
            _tickService.Register(_positioner);
            Debug.Log("Positioner registred");

            _positioner.Completed += OnPositionerCompleted;
            _creator.Created += OnCreated;

            _creator.Create();
        }

        public void Exit()
        {
            _creationView.Hide();
            _creatorPresenter.Dispose();
            _transformator.Dispose();
            _tickService.Remove(_positioner);

            _positioner.Completed -= OnPositionerCompleted;
            _creator.Created -= OnCreated;
        }

        private void OnPositionerCompleted() =>
            _stateMachine.Enter<EnvironmentObjectTransformationState, EnvironmentObject>(_creator.CurrentObject);

        private void OnCreated() =>
            _transformator.SetObject(_creator.CurrentObject);
    }
}
