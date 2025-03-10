using System;
using UnityEngine;

namespace Assets.Sources.BaseLogic.EnvironmentObject
{
    public class EnvironmentObject : MonoBehaviour
    {
        private const float MinScale = 0.5f;
        private const float MaxScale = 1.5f;
        private const float ScaleFactor = 0.001f;

        [SerializeField] private Renderer _renderer;

        private float _scale;
        private string _colorPropertyName;

        public Color Color { get; private set; }

        private void Start()
        {
            _scale = 1;
            Color = new Color(1, 1, 1);
        }

        public void Initialize(string colorPropertyName)
        {
            _colorPropertyName = colorPropertyName;

            Debug.Log("has property name " + _renderer.material.HasColor(_colorPropertyName));
        }

        public void SetColor(Color color)
        {
            Color = color;
            Debug.Log("Set color " + Color);
            Material[] materials = _renderer.materials;

            foreach(Material material in materials)
                material.SetColor(_colorPropertyName, Color);

            _renderer.materials = materials;
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
