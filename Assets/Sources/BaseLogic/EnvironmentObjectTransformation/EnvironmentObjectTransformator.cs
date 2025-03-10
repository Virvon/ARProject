using Assets.Sources.Services.InputService;
using System;
using UnityEngine;

namespace Assets.Sources.BaseLogic.EnvironmentObjectTransformation
{
    public class EnvironmentObjectTransformator : IDisposable
    {
        private const float RaycastDistace = 100;

        private readonly IInputService _inputService;
        private readonly Camera _camera;

        private EnvironmentObject.EnvironmentObject _environmentObject;

        private bool _needRotated;
        private float _lastHorizontalPosition;

        public EnvironmentObjectTransformator(IInputService inputService, Camera camera)
        {
            _inputService = inputService;
            _camera = camera;

            _inputService.Clicked += OnClicked;
            _inputService.Dragged += OnDragged;
            _inputService.DragEnded += OnDragEnded;
            _inputService.Zoomed += OnZoomed;
        }

        public void Dispose()
        {
            _environmentObject = null;

            _inputService.Clicked -= OnClicked;
            _inputService.Dragged -= OnDragged;
            _inputService.DragEnded -= OnDragEnded;
            _inputService.Zoomed -= OnZoomed;
        }

        public void SetObject(EnvironmentObject.EnvironmentObject environmentObject)
        {
            Debug.Log("Transformator object changed");
            _environmentObject = environmentObject;
            _needRotated = false;
        }

        private void OnDragEnded() =>
            _needRotated = false;

        private void OnClicked(Vector2 position)
        {
            if (_environmentObject == null)
                return;

            Ray ray = _camera.ScreenPointToRay(position);
            bool isHitted = Physics.Raycast(ray, out RaycastHit hitInfo, RaycastDistace);

            if (isHitted)
            {
                bool isEnvironmentObject = hitInfo.transform.TryGetComponent(out EnvironmentObject.EnvironmentObject environmentObject);

                if ((isEnvironmentObject && environmentObject != _environmentObject) || isEnvironmentObject == false)
                    StartRotate(position);
            }
            else
            {
                StartRotate(position);
            }
        }

        private void StartRotate(Vector2 position)
        {
            _needRotated = true;
            _lastHorizontalPosition = position.x;
        }

        private void OnDragged(Vector2 position)
        {
            if(_needRotated)
            {
                float delta = _lastHorizontalPosition - position.x;
                _lastHorizontalPosition = position.x;

                _environmentObject.transform.rotation *= Quaternion.Euler(0, delta, 0);
            }
        }

        private void OnZoomed(float delta) =>
            _environmentObject.Scale(delta);
    }
}
