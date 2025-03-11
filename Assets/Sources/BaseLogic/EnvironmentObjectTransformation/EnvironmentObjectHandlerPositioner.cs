using Assets.Sources.BaseLogic.EnvironmentObject;
using Assets.Sources.Services.InputService;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Assets.Sources.BaseLogic.EnvironmentObjectTransformation
{
    public class EnvironmentObjectHandlerPositioner : IDisposable
    {
        private const float RaycastDistace = 100;

        private readonly IInputService _inputService;
        private readonly ARRaycastManager _raycastManager;
        private readonly Camera _camera;
        private readonly EnvironmentObject.EnvironmentObject _environmentObject;
        private readonly Pointer _pointer;

        private bool _needReplaced;

        public EnvironmentObjectHandlerPositioner(
            IInputService inputService,
            ARRaycastManager raycastManager,
            Camera camera,
            EnvironmentObject.EnvironmentObject environmentObject,
            Pointer pointer)
        {
            _inputService = inputService;
            _raycastManager = raycastManager;
            _camera = camera;
            _environmentObject = environmentObject;
            _pointer = pointer;

            _inputService.Clicked += OnClicked;
            _inputService.Dragged += OnDragged;
            _inputService.DragEnded += OnDragEnded;

            _pointer.transform.position = _environmentObject.transform.position;
        }

        public void Dispose()
        {
            _inputService.Clicked -= OnClicked;
            _inputService.Dragged -= OnDragged;
            _inputService.DragEnded -= OnDragEnded;
        }

        private void OnClicked(Vector2 position)
        {
            Ray ray = _camera.ScreenPointToRay(position);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, RaycastDistace)
                && hitInfo.transform.TryGetComponent(out EnvironmentObject.EnvironmentObject environmentObject)
                && environmentObject == _environmentObject)
                _needReplaced = true;
        }

        private void OnDragged(Vector2 handlePosition)
        {
            if (_needReplaced)
            {
                List<ARRaycastHit> hits = new();

                if (_raycastManager.Raycast(handlePosition, hits))
                {
                    Vector3 position = hits[0].pose.position;

                    _environmentObject.transform.position = position;
                    _pointer.transform.position = position;
                }
            }
        }

        private void OnDragEnded() =>
            _needReplaced = false;
    }
}
