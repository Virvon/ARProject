using Assets.Sources.BaseLogic.EnvironmentObjectCreation;
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
        private readonly EnvironmentObjectCreator _creator;
        private readonly ARRaycastManager _raycastManager;
        private readonly Camera _camera;

        private bool _needReplaced;
        private bool _isActive;

        public EnvironmentObjectHandlerPositioner(
            IInputService inputService,
            EnvironmentObjectCreator creator,
            ARRaycastManager raycastManager,
            Camera camera)
        {
            _inputService = inputService;
            _creator = creator;
            _raycastManager = raycastManager;
            _camera = camera;

            _isActive = false;

            _inputService.Clicked += OnClicked;
            _inputService.Dragged += OnDragged;
            _inputService.DragEnded += OnDragEnded;
        }

        public void Dispose()
        {
            _inputService.Clicked -= OnClicked;
            _inputService.Dragged -= OnDragged;
            _inputService.DragEnded -= OnDragEnded;
        }

        public void SetActive(bool isActive) =>
            _isActive = isActive;

        private void OnClicked(Vector2 position)
        {
            if (_isActive == false)
                return;

            Ray ray = _camera.ScreenPointToRay(position);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, RaycastDistace)
                && hitInfo.transform.TryGetComponent(out EnvironmentObject.EnvironmentObject environmentObject)
                && environmentObject == _creator.CurrentObject)
                _needReplaced = true;
        }

        private void OnDragged(Vector2 position)
        {
            if (_isActive == false)
                return;

            if (_needReplaced)
            {
                List<ARRaycastHit> hits = new();

                if (_raycastManager.Raycast(position, hits))
                    _creator.CurrentObject.transform.position = hits[0].pose.position;
            }
        }

        private void OnDragEnded() =>
            _needReplaced = false;
    }
}
