using Assets.Sources.ApplicationStateMachine;
using Assets.Sources.ApplicationStateMachine.States;
using Assets.Sources.BaseLogic.EnvironmentObjectCreation;
using Assets.Sources.StaticDataService;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Assets.Sources.CompositionRoot
{
    public class CompositionRoot : MonoBehaviour
    {
        [SerializeField] private ARRaycastManager _raycastManager;
        [SerializeField] private Camera _camera;

        private EnvironmentObjectCreator _environmentObjectCreator;
        CreatingEnvironmentObjectPositioner _creatingEnvironmentObjectPositioner;

        private void Awake()
        {
            IStaticDataService staticDataService = new StaticDataService.StaticDataService();
            staticDataService.Initialize();

            _environmentObjectCreator = new (staticDataService);
            _creatingEnvironmentObjectPositioner = new(_environmentObjectCreator, _raycastManager, _camera);

            InitializeApplicationStateMachine();
        }

        private void InitializeApplicationStateMachine()
        {
            StateMachine stateMachine = new();

            EnvironmentObjectCreationState environmentObjectCreationState = new (_environmentObjectCreator);

            stateMachine.RegisterStates(environmentObjectCreationState);

            stateMachine.Enter<EnvironmentObjectCreationState>();
        }

        private void Update()
        {
            if (_creatingEnvironmentObjectPositioner != null)
                _creatingEnvironmentObjectPositioner.Tick();
        }
    }
}