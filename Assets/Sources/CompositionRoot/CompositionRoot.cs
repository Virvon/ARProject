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

        private void Awake()
        {
            _tickService = new();

            InitializeStaticDataService();
            InitializeInputService();
            InitializePointer();
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

            InformationState informationState = new(stateMachine, _informationView);
            ReviewState reviewState = new(_reviewView, stateMachine, _inputService, _camera);

            EnvironmentObjectCreationState environmentObjectCreationState = new(
                _staticDataService,
                _raycastManager,
                _camera,
                _inputService,
                _creationView,
                stateMachine,
                _tickService,
                _pointer);

            EnvironmentObjectTransformationState environmentObjectTransformationState = new(
                _inputService,
                _raycastManager,
                _camera,
                _transformationView,
                stateMachine,
                _pointer);

            ColorSelectionState colorSelectionState = new(_inputService, _colorSelectionView, stateMachine, _pointer);

            stateMachine.RegisterState(informationState);
            stateMachine.RegisterState(reviewState);
            stateMachine.RegisterState(environmentObjectCreationState);
            stateMachine.RegisterState(environmentObjectTransformationState);
            stateMachine.RegisterState(colorSelectionState);

            stateMachine.Enter<InformationState>();
        }

        private void Update()
        {
            try
            {
                _tickService.Tick();
            }
            catch
            {
                Debug.Log("Tick exseption-------------------------------------");
            }
        }
    }
}