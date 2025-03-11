using Assets.Sources.BaseLogic.EnvironmentObject;
using Assets.Sources.BaseLogic.EnvironmentObjectTransformation;
using Assets.Sources.Services.InputService;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Assets.Sources.ApplicationStateMachine.States
{
    public class EnvironmentObjectTransformationState : IPayloadState<EnvironmentObject>
    {
        private readonly IInputService _inputService;
        private readonly ARRaycastManager _raycastManager;
        private readonly Camera _camera;
        private readonly TransformationView _transformationView;
        private readonly StateMachine _stateMachine;
        private readonly Pointer _pointer;

        private EnvironmentObjectTransformator _transformator;
        private EnvironmentObjectHandlerPositioner _positioner;
        private TransformationPresenter _transformationPresenter;

        public EnvironmentObjectTransformationState(
            IInputService inputService,
            ARRaycastManager raycastManager,
            Camera camera,
            TransformationView transformationView,
            StateMachine stateMachine,
            Pointer pointer)
        {
            _inputService = inputService;
            _raycastManager = raycastManager;
            _camera = camera;
            _transformationView = transformationView;
            _stateMachine = stateMachine;
            _pointer = pointer;
        }

        public void Enter(EnvironmentObject environmentObject)
        {
            Debug.Log("Enter transformation state");

            _transformator = new(_inputService, _camera);
            _positioner = new(_inputService, _raycastManager, _camera, environmentObject, _pointer);
            TransformationModel transformationModel = new(_stateMachine, environmentObject);
            _transformationPresenter = new(transformationModel, _transformationView);

            _transformationView.Show();
            _transformator.SetObject(environmentObject);
            _pointer.SetState(true);
            _pointer.gameObject.SetActive(true);

            Debug.Log("Finis entered to transformation state");
        }

        public void Exit()
        {
            _transformator.Dispose();
            _positioner.Dispose();
            _transformationPresenter.Dispose();
            _transformationView.Hide();
            _pointer.gameObject.SetActive(false);
        }
    }
}
