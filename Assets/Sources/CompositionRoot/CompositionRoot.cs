using Assets.Sources.ApplicationStateMachine;
using Assets.Sources.ApplicationStateMachine.States;
using Assets.Sources.BaseLogic;
using Assets.Sources.BaseLogic.ColorSelection;
using Assets.Sources.BaseLogic.EnvironmentObject;
using Assets.Sources.BaseLogic.EnvironmentObjectCreation;
using Assets.Sources.BaseLogic.EnvironmentObjectTransformation;
using Assets.Sources.BaseLogic.Information;
using Assets.Sources.Services.InputService;
using Assets.Sources.Services.TickService;
using Assets.Sources.SharedBundle;
using Assets.Sources.StaticDataService;
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
        [SerializeField] private ColorSelectionView _colorSelectionView;
        [SerializeField] private Pointer _pointerPrefab;
        [SerializeField] private InformationView _informationView;

        private TickService _tickService;
        private IStaticDataService _staticDataService;
        private IInputService _inputService;
        private Pointer _pointer;
        private SharedBundle.SharedBundle _sharedBundle;

        private void Awake()
        {
            InitializeTickService();
            InitializeStaticDataService();
            InitializeInputService();
            InitializePointer();
            InitializeSharedBundle();
            InitializeApplicationStateMachine();
        }

        private void InitializePointer() =>
            _pointer = Instantiate(_pointerPrefab);

        private void InitializeStaticDataService()
        {
            _staticDataService = new StaticDataService.StaticDataService();
            _staticDataService.Initialize();
        }

        private void InitializeInputService()
        {
            _inputService = new InputService();
            _tickService.Register(_inputService);
        }

        private void InitializeApplicationStateMachine()
        {
            StateMachine stateMachine = new();

            InformationState informationState = new(stateMachine, _sharedBundle);
            ReviewState reviewState = new(stateMachine, _sharedBundle);
            EnvironmentObjectCreationState environmentObjectCreationState = new(stateMachine, _sharedBundle);
            EnvironmentObjectTransformationState environmentObjectTransformationState = new(stateMachine, _sharedBundle);
            ColorSelectionState colorSelectionState = new(stateMachine, _sharedBundle);

            stateMachine.Register(informationState);
            stateMachine.Register(reviewState);
            stateMachine.Register(environmentObjectCreationState);
            stateMachine.Register(environmentObjectTransformationState);
            stateMachine.Register(colorSelectionState);

            stateMachine.Enter<InformationState>();
        }

        private void InitializeTickService() =>
            _tickService = new();

        private void InitializeSharedBundle()
        {
            _sharedBundle = new();

            _sharedBundle.Add(SharedBundleKeys.RaycastManager, _raycastManager);
            _sharedBundle.Add(SharedBundleKeys.StaticDataService, _staticDataService);
            _sharedBundle.Add(SharedBundleKeys.Camera, _camera);
            _sharedBundle.Add(SharedBundleKeys.InputService, _inputService);
            _sharedBundle.Add(SharedBundleKeys.CreationView, _creationView);
            _sharedBundle.Add(SharedBundleKeys.TickService, _tickService);
            _sharedBundle.Add(SharedBundleKeys.Pointer, _pointer);
            _sharedBundle.Add(SharedBundleKeys.ColorSelectionView, _colorSelectionView);
            _sharedBundle.Add(SharedBundleKeys.TransformatinView, _transformationView);
            _sharedBundle.Add(SharedBundleKeys.InformationView, _informationView);
            _sharedBundle.Add(SharedBundleKeys.ReviewView, _reviewView);
        }

        private void Update() =>
            _tickService.Tick();
    }
}