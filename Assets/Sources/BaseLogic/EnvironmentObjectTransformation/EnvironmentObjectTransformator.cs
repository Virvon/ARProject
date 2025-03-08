using Assets.Sources.BaseLogic.EnvironmentObjectCreation;
using Assets.Sources.Services.InputService;
using System;
using System.Collections.Generic;
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

        private EnvironmentObject _currentObject;

        private bool _needReplaced;
        private float _lastHorizontalPosition;
        private float _scale;

        public EnvironmentObjectTransformator(IInputService inputService, Camera camera, ARRaycastManager raycastManager)
        {
            _inputService = inputService;
            _camera = camera;
            _raycastManager = raycastManager;

            _needReplaced = false;
            _scale = 1;
        }

        public void Dispose()
        {
            _inputService.Clicked -= OnClicked;
            _inputService.Dragged -= OnDragged;
            _inputService.DragEnded -= OnDragEnded;
            _inputService.Zoomed -= OnZoomed;
        }

        public void SetObject(EnvironmentObject environmentObject)
        {
            _currentObject = environmentObject;

            _inputService.Clicked += OnClicked;
            _inputService.Dragged += OnDragged;
            _inputService.DragEnded += OnDragEnded;
            _inputService.Zoomed += OnZoomed;
        }

        private void OnDragEnded()
        {
            _needReplaced = false;
        }

        private void OnClicked(Vector2 position)
        {
            Ray ray = _camera.ScreenPointToRay(position);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, RaycastDistace)
                && hitInfo.transform.TryGetComponent(out EnvironmentObject environmentObject)
                && environmentObject == _currentObject)
                _needReplaced = true;
            else
                _lastHorizontalPosition = position.x;
        }

        private void OnDragged(Vector2 position)
        {
            if (_needReplaced)
            {
                List<ARRaycastHit> hits = new();

                if (_raycastManager.Raycast(position, hits))
                    _currentObject.transform.position = hits[0].pose.position;
            }
            else
            {
                float delta = _lastHorizontalPosition - position.x;
                _lastHorizontalPosition = position.x;

                _currentObject.transform.rotation *= Quaternion.Euler(0, delta, 0);
            }
        }

        private void OnZoomed(float delta)
        {
            _scale += delta * 0.0001f;

            _scale = Mathf.Clamp(_scale, 0.2f, 3f);

            _currentObject.transform.localScale = Vector3.one * _scale;

            Debug.Log(_scale + " " + _currentObject.transform.localScale + " " + delta);
        }
    }
}
