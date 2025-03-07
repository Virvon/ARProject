using System;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;

namespace Assets.Sources
{
    public class Test : MonoBehaviour
    {
        public XROrigin Origin;
        public TMP_Text _text1;
        public TMP_Text _text2;

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
            _text1.text = "changed " + x++;
            _text2.text = obj.TrackablesParent.name;
        }
    }
}
