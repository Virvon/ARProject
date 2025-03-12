using Assets.Sources.BaseLogic.EnvironmentObject;
using Assets.Sources.BaseLogic.EnvironmentObjectCreation;
using Assets.Sources.BaseLogic.EnvironmentObjectTransformation;
using Assets.Sources.Services.InputService;
using Assets.Sources.Services.TickService;
using Assets.Sources.SharedBundle;
using Assets.Sources.StaticDataService;
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

        public EnvironmentObjectCreationState(StateMachine stateMachine, SharedBundle sharedBundle)
        {
            _stateMachine = stateMachine;
            _staticDataService = sharedBundle.Get<IStaticDataService>(SharedBundleKeys.StaticDataService);
            _raycastManager = sharedBundle.Get<ARRaycastManager>(SharedBundleKeys.RaycastManager);
            _camera = sharedBundle.Get<Camera>(SharedBundleKeys.Camera);
            _inputService = sharedBundle.Get<IInputService>(SharedBundleKeys.InputService);
            _creationView = sharedBundle.Get<CreationView>(SharedBundleKeys.CreationView);
            _tickService = sharedBundle.Get<TickService>(SharedBundleKeys.TickService);
            _pointer = sharedBundle.Get<Pointer>(SharedBundleKeys.Pointer);
        }

        public void Enter()
        {
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
