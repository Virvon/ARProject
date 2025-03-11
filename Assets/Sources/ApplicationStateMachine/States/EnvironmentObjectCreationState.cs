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
        private readonly Pointer _pointer;

        EnvironmentObjectCameraPositioner _positioner;
        CreatorPresenter _creatorPresenter;
        EnvironmentObjectTransformator _transformator;
        CreationModel _creationModel;

        public EnvironmentObjectCreationState(
            IStaticDataService staticDataService,
            ARRaycastManager raycastManager,
            Camera camera,
            IInputService inputService,
            CreationView creationView,
            StateMachine stateMachine,
            TickService tickService,
            Pointer pointer)
        {
            _staticDataService = staticDataService;
            _raycastManager = raycastManager;
            _camera = camera;
            _inputService = inputService;
            _creationView = creationView;
            _stateMachine = stateMachine;
            _tickService = tickService;
            _pointer = pointer;
        }

        public void Enter()
        {
            Debug.Log("Enter to creation state");

            EnvironmentObjectCreator creator = new(_staticDataService);
            _positioner = new(creator, _raycastManager, _camera, _inputService, _pointer);
            _transformator = new(_inputService, _camera);
            _creationModel = new(_stateMachine, creator, _positioner, _transformator);
            _creatorPresenter = new(_creationView, _creationModel);

            _creationView.Show();
            _tickService.Register(_positioner);
            _pointer.gameObject.SetActive(true);

            creator.Create();
        }

        public void Exit()
        {
            _creationView.Hide();
            _creatorPresenter.Dispose();
            _transformator.Dispose();
            _creationModel.Dispose();
            _tickService.Remove(_positioner);
            _pointer.gameObject.SetActive(false);
        }
    }
}
