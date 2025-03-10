using Assets.Sources.Services.InputService;
using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Assets.Sources.BaseLogic.EnvironmentObjectTransformation
{
    public class EnvironmentObjectTransformator : IDisposable
    {
        private const float RaycastDistace = 100;

        private readonly IInputService _inputService;
        private readonly Camera _camera;
        private readonly ARRaycastManager _raycastManager;

        private EnvironmentObject.EnvironmentObject _currentObject;

        private bool _needRotated;
        private float _lastHorizontalPosition;
        private float _scale;
        private bool _isActive;

        public EnvironmentObjectTransformator(IInputService inputService, Camera camera, ARRaycastManager raycastManager)
        {
            _inputService = inputService;
            _camera = camera;
            _raycastManager = raycastManager;

            _inputService.Clicked += OnClicked;
            _inputService.Dragged += OnDragged;
            _inputService.DragEnded += OnDragEnded;
            _inputService.Zoomed += OnZoomed;
        }

        public void Dispose()
        {
            _inputService.Clicked -= OnClicked;
            _inputService.Dragged -= OnDragged;
            _inputService.DragEnded -= OnDragEnded;
            _inputService.Zoomed -= OnZoomed;
        }

        public void SetActive(bool isActive) =>
            _isActive = isActive;

        private void OnDragEnded() =>
            _needRotated = false;

        private void OnClicked(Vector2 position)
        {
            if (_isActive == false)
                return;

            Ray ray = _camera.ScreenPointToRay(position);
            bool isHitted = Physics.Raycast(ray, out RaycastHit hitInfo, RaycastDistace);

            if ((isHitted
                && hitInfo.transform.TryGetComponent(out EnvironmentObject.EnvironmentObject environmentObject)
                && environmentObject != _currentObject)
                || isHitted == false)
            {
                _needRotated = true;
                _lastHorizontalPosition = position.x;
            }                
        }

        private void OnDragged(Vector2 position)
        {
            if(_needRotated && _isActive)
            {
                float delta = _lastHorizontalPosition - position.x;
                _lastHorizontalPosition = position.x;

                _currentObject.transform.rotation *= Quaternion.Euler(0, delta, 0);
            }
        }

        private void OnZoomed(float delta)
        {
            return;

            _scale += delta * 0.0001f;

            _scale = Mathf.Clamp(_scale, 0.2f, 3f);

            _currentObject.transform.localScale = Vector3.one * _scale;

            Debug.Log(_scale + " " + _currentObject.transform.localScale + " " + delta);
        }
    }
}
