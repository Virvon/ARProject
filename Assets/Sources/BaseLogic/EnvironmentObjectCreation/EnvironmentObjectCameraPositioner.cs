using Assets.Sources.BaseLogic.EnvironmentObject;
using Assets.Sources.Services.InputService;
using Assets.Sources.Services.TickService;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Assets.Sources.BaseLogic.EnvironmentObjectCreation
{
    public class EnvironmentObjectCameraPositioner : ITickable, IDisposable
    {
        private const float RayCastDistance = 100;
        private const float StartPointerDistance = 5;

        private readonly EnvironmentObjectCreator _creator;
        private readonly ARRaycastManager _raycastManager;
        private readonly Camera _camera;
        private readonly IInputService _inputService;
        private readonly Pointer _pointer;

        private float _pointerDistance;

        public EnvironmentObjectCameraPositioner(
            EnvironmentObjectCreator creator,
            ARRaycastManager raycastManager,
            Camera camera,
            IInputService inputService,
            Pointer pointer)
        {
            _creator = creator;
            _raycastManager = raycastManager;
            _camera = camera;
            _inputService = inputService;
            _pointer = pointer;

            _pointerDistance = StartPointerDistance;

            _inputService.Clicked += OnClicked;
        }

        public event Action Completed;

        public void Dispose()
        {
            _inputService.Clicked -= OnClicked;
        }

        public void Tick()
        {
            if (_creator.CurrentObject == null)
                return;

            Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            List<ARRaycastHit> hits = new ();

            if (_raycastManager.Raycast(ray, hits, TrackableType.Planes))
            {
                Vector3 position = hits[0].pose.position;

                _creator.CurrentObject.transform.position = position;
                _pointer.transform.position = position;
                _pointerDistance = (_camera.transform.position - position).magnitude;

                _pointer.SetState(true);
                _creator.CurrentObject.SetActive(true);
            }
            else
            {
                _pointer.transform.position = _camera.transform.position + _camera.transform.forward * _pointerDistance;

                _pointer.SetState(false);
                _creator.CurrentObject.SetActive(false);
            }
        }

        private void OnClicked(Vector2 position)
        {
            Ray ray = _camera.ScreenPointToRay(position);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, RayCastDistance)
                && hitInfo.transform.TryGetComponent(out EnvironmentObject.EnvironmentObject environmentObject)
                && environmentObject == _creator.CurrentObject)
                Completed?.Invoke();
        }
    }
}
