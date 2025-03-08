using Assets.Sources.ApplicationStateMachine;
using Assets.Sources.ApplicationStateMachine.States;
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

        private EnvironmentObjectCreator _environmentObjectCreator;
        private CreatingEnvironmentObjectPositioner _creatingEnvironmentObjectPositioner;
        private EnvironmentObjectTransformator _environmentObjectTranformator;

        private List<ITickable> _tickables;

        private void Awake()
        {
            _tickables = new();

            IStaticDataService staticDataService = new StaticDataService.StaticDataService();
            staticDataService.Initialize();

            IInputService inputService = new InputService();
            _tickables.Add(inputService);

            _environmentObjectCreator = new (staticDataService);
            _creatingEnvironmentObjectPositioner = new(_environmentObjectCreator, _raycastManager, _camera, inputService);
            _tickables.Add(_creatingEnvironmentObjectPositioner);

            _environmentObjectTranformator = new(inputService, _camera, _raycastManager);

            InitializeApplicationStateMachine();
        }

        private void OnDestroy()
        {
            _creatingEnvironmentObjectPositioner.Dispose();
        }

        private void InitializeApplicationStateMachine()
        {
            StateMachine stateMachine = new();

            EnvironmentObjectCreationState environmentObjectCreationState = new (_environmentObjectCreator, _creatingEnvironmentObjectPositioner, stateMachine);
            EnvironmentObjectTransformationState environmentObjectTransformationState = new(_environmentObjectTranformator);

            stateMachine.RegisterState(environmentObjectCreationState);
            stateMachine.RegisterState(environmentObjectTransformationState);
            
            stateMachine.Enter<EnvironmentObjectCreationState>();
        }

        private void Update()
        {
            foreach (ITickable tickable in _tickables)
                tickable.Tick();
        }
    }
}