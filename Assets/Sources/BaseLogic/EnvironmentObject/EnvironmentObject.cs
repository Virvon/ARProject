using System;
using UnityEngine;

namespace Assets.Sources.BaseLogic.EnvironmentObject
{
    public class EnvironmentObject : MonoBehaviour
    {
        private const float MinScale = 0.5f;
        private const float MaxScale = 1.5f;
        private const float ScaleFactor = 0.001f;

        private float _scale;

        private void Start()
        {
            _scale = 1;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public void Scale(float value)
        {
            _scale += value * ScaleFactor;

            _scale = Mathf.Clamp(_scale, MinScale, MaxScale);

            transform.localScale = Vector3.one * _scale;
        }
    }
}
