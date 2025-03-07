using System;
using Unity.XR.CoreUtils;
using UnityEngine;

namespace Assets.Sources
{
    public class Test : MonoBehaviour
    {
        public XROrigin Origin;

        public void OnEnable()
        {
            Debug.Log("enable");
            Origin.TrackablesParentTransformChanged += X;
        }

        public void OnDisable()
        {
            Origin.TrackablesParentTransformChanged -= X;
        }

        int x;

        private void X(ARTrackablesParentTransformChangedEventArgs obj)
        {
            Debug.Log("changed " + x++);
            Debug.Log(obj.TrackablesParent.name);
        }
    }
}
