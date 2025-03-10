using Assets.Sources.ApplicationStateMachine;
using Assets.Sources.ApplicationStateMachine.States;
using Assets.Sources.BaseLogic;
using Assets.Sources.BaseLogic.EnvironmentObjectCreation;
using Assets.Sources.BaseLogic.EnvironmentObjectTransformation;
using Assets.Sources.Services.InputService;
using Assets.Sources.StaticDataService;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Assets.Sources.CompositionRoot
{
    public class CompositionRoot : MonoBehaviour
    {
        [SerializeField] private ARRaycastManager _raycastManager;
        [SerializeField] private Camera _camera;
        [SerializeField] private ReviewView _reviewView;
        [SerializeField] private CreationView _creationView;
        [SerializeField] private TransformationView _transformationView;

        private StateMachine _stateMachine;
        private Review _review;
        private EnvironmentObjectCreator _environmentObjectCreator;
        private EnvironmentObjectCameraPositioner _creatingEnvironmentObjectCameraPositioner;
        private EnvironmentObjectTransformator _environmentObjectTranformator;
        private ReviewPresenter _reviewPresenter;
        private CreatorPresenter _creatorPresenter;
        private EnvironmentObjectHandlerPositioner _environmentObjectHandlerPositioner;
        private TransformationPresenter _transformationPresenter;

        private List<ITickable> _tickables;

        private void Awake()
        {
            _tickables = new();
            _stateMachine = new();

            IStaticDataService staticDataService = new StaticDataService.StaticDataService();
            staticDataService.Initialize();

            IInputService inputService = new InputService();
            _tickables.Add(inputService);

            InitializeEnvironmentObjectTransformator(inputService);
            InitializeReview();
            InitializeEnviromentObjectCreator(staticDataService, inputService);


            InitializeApplicationStateMachine();
        }

        private void OnDestroy() =>
            _stateMachine.Enter<EndState>();

        private void InitializeReview()
        {
            _review = new(_stateMachine);
            _reviewPresenter = new(_review, _reviewView);
        }

        private void InitializeEnviromentObjectCreator(IStaticDataService staticDataService, IInputService inputService)
        {
            _environmentObjectCreator = new(staticDataService);
            _creatingEnvironmentObjectCameraPositioner = new(_environmentObjectCreator, _raycastManager, _camera, inputService);
            _creatorPresenter = new(_creationView, _environmentObjectCreator, _creatingEnvironmentObjectCameraPositioner);

            _tickables.Add(_creatingEnvironmentObjectCameraPositioner);
        }

        private void InitializeEnvironmentObjectTransformator(IInputService inputService)
        {
            _environmentObjectTranformator = new(inputService, _camera, _raycastManager);
            _environmentObjectHandlerPositioner = new(inputService, _environmentObjectCreator, _raycastManager, _camera);
            TransformationModel transformationModel = new(_stateMachine);
            _transformationPresenter = new(transformationModel, _transformationView);

        }

        private void InitializeApplicationStateMachine()
        {
            ReviewState reviewState = new(_review);
            EnvironmentObjectCreationState environmentObjectCreationState = new (
                _environmentObjectCreator,
                _creatingEnvironmentObjectCameraPositioner,
                _stateMachine,
                _environmentObjectTranformator);
            EnvironmentObjectTransformationState environmentObjectTransformationState = new(_environmentObjectTranformator, _environmentObjectHandlerPositioner, _transformationView);
            EndState endState = new(
                _reviewPresenter,
                _creatingEnvironmentObjectCameraPositioner,
                _creatorPresenter,
                _environmentObjectTranformator,
                _transformationPresenter);

            _stateMachine.RegisterState(reviewState);
            _stateMachine.RegisterState(environmentObjectCreationState);
            _stateMachine.RegisterState(environmentObjectTransformationState);
            _stateMachine.RegisterState(endState);

            _stateMachine.Enter<ReviewState>();
        }

        private void Update()
        {
            foreach (ITickable tickable in _tickables)
                tickable.Tick();
        }
    }
}