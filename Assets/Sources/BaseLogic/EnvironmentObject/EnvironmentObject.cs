using Assets.Sources.BaseLogic.EnvironmentObject.ColorChanging;
using UnityEngine;

namespace Assets.Sources.BaseLogic.EnvironmentObject
{
    public class EnvironmentObject : MonoBehaviour
    {
        private const float MinScale = 0.5f;
        private const float MaxScale = 1.5f;
        private const float ScaleFactor = 0.001f;

        [SerializeField] private ColorChanger _colorChanger;

        private float _scale;

        public ColorChanger ColorChanger => _colorChanger;

        private void Start() =>
            _scale = 1;

        public void Initialize(string colorPropertyName) =>
            _colorChanger.Initialize(colorPropertyName);

        public void SetColor(Color color) =>
            _colorChanger.SetColor(color);

        public void Destroy() =>
            Destroy(gameObject);

        public void Scale(float value)
        {
            _scale += value * ScaleFactor;

            _scale = Mathf.Clamp(_scale, MinScale, MaxScale);

            transform.localScale = Vector3.one * _scale;
        }
    }
}
