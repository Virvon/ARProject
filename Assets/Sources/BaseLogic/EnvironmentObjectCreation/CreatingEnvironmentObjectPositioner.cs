using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Assets.Sources.BaseLogic.EnvironmentObjectCreation
{
    public class CreatingEnvironmentObjectPositioner : ITikable
    {
        private readonly EnvironmentObjectCreator _creator;
        private readonly ARRaycastManager _raycastManager;
        private readonly Camera _camera;

        public CreatingEnvironmentObjectPositioner(EnvironmentObjectCreator creator, ARRaycastManager raycastManager, Camera camera)
        {
            _creator = creator;
            _raycastManager = raycastManager;
            _camera = camera;
        }

        public void Tick()
        {
            if (_creator.CurrentObject == null)
                return;

            Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            List<ARRaycastHit> hits = new List<ARRaycastHit>();

            if (_raycastManager.Raycast(ray, hits, TrackableType.Planes))
            {
                _creator.CurrentObject.transform.position = hits[0].pose.position;
                _creator.CurrentObject.transform.rotation = hits[0].pose.rotation;
            }
        }
    }
}
