using Assets.Sources.ApplicationStateMachine;
using Assets.Sources.ApplicationStateMachine.States;
using Assets.Sources.BaseLogic;
using Assets.Sources.BaseLogic.ColorSelection;
using Assets.Sources.BaseLogic.EnvironmentObjectCreation;
using Assets.Sources.BaseLogic.EnvironmentObjectTransformation;
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

        private TickService _tickService;
        private IStaticDataService _staticDataService;
        private IInputService _inputService;


        private void Awake()
        {
            _tickService = new();

            InitializeStaticDataService();
            InitializeInputService();
            InitializeApplicationStateMachine();
        }

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

            ReviewState reviewState = new(_reviewView, stateMachine);
            EnvironmentObjectCreationState environmentObjectCreationState = new(_staticDataService, _raycastManager, _camera, _inputService, _creationView, stateMachine, _tickService);
            EnvironmentObjectTransformationState environmentObjectTransformationState = new(_inputService, _raycastManager, _camera, _transformationView, stateMachine);
            ColorSelectionState colorSelectionState = new(_inputService, _colorSelectionView, stateMachine);

            stateMachine.RegisterState(reviewState);
            stateMachine.RegisterState(environmentObjectCreationState);
            stateMachine.RegisterState(environmentObjectTransformationState);
            stateMachine.RegisterState(colorSelectionState);

            stateMachine.Enter<ReviewState>();
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