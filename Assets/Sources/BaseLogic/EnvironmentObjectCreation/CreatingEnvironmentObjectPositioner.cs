﻿using Assets.Sources.ApplicationStateMachine;
using Assets.Sources.ApplicationStateMachine.States;
using Assets.Sources.Services.InputService;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Assets.Sources.BaseLogic.EnvironmentObjectCreation
{
    public class CreatingEnvironmentObjectPositioner : ITickable, IDisposable
    {
        private const float RayCastDistance = 100;

        private readonly EnvironmentObjectCreator _creator;
        private readonly ARRaycastManager _raycastManager;
        private readonly Camera _camera;
        private readonly IInputService _inputService;

        public CreatingEnvironmentObjectPositioner(
            EnvironmentObjectCreator creator,
            ARRaycastManager raycastManager,
            Camera camera,
            IInputService inputService)
        {
            _creator = creator;
            _raycastManager = raycastManager;
            _camera = camera;
            _inputService = inputService;

            _inputService.Clicked += OnClicked;
        }

        public event Action<EnvironmentObject> Completed;

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
                _creator.CurrentObject.transform.position = hits[0].pose.position;
                _creator.CurrentObject.transform.rotation = hits[0].pose.rotation;
            }
        }

        private void OnClicked(Vector2 position)
        {
            Ray ray = _camera.ScreenPointToRay(position);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, RayCastDistance)
                && hitInfo.transform.TryGetComponent(out EnvironmentObject environmentObject))
            {
                Completed?.Invoke(environmentObject);
                _creator.Reset();
            }
        }
    }
}
