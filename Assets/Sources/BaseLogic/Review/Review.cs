using Assets.Sources.ApplicationStateMachine;
using Assets.Sources.ApplicationStateMachine.States;
using Assets.Sources.Services.InputService;
using System;
using UnityEngine;

namespace Assets.Sources.BaseLogic
{
    public class Review : IDisposable
    {
        private const float RaycastDistance = 100;

        private readonly StateMachine _stateMachine;
        private readonly IInputService _inputService;
        private readonly Camera _camera;

        public Review(StateMachine stateMachine, IInputService inputService, Camera camera)
        {
            _stateMachine = stateMachine;
            _inputService = inputService;

            _inputService.Clicked += OnClicked;
            _camera = camera;
        }

        public void Dispose()
        {
            _inputService.Clicked -= OnClicked;
        }

        public void MoveNextState() =>
            _stateMachine.Enter<EnvironmentObjectCreationState>();

        private void OnClicked(Vector2 position)
        {
            Ray ray = _camera.ScreenPointToRay(position);

            if(Physics.Raycast(ray, out RaycastHit hitInfo, RaycastDistance)
                && hitInfo.transform.TryGetComponent(out EnvironmentObject.EnvironmentObject environmentObject))
                _stateMachine.Enter<EnvironmentObjectTransformationState, EnvironmentObject.EnvironmentObject>(environmentObject);
        }
    }
}
