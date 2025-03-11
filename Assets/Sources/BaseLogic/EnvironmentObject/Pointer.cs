using UnityEngine;

namespace Assets.Sources.BaseLogic.EnvironmentObject
{
    public class Pointer : MonoBehaviour
    {
        private const string ColorPropertyName = "_Color";

        [SerializeField] private Color _validColor;
        [SerializeField] private Color _invalidColor;
        [SerializeField] private Renderer _renderer;

        private bool _isPointValid;

        private void Start()
        {
            gameObject.SetActive(false);
            _isPointValid = true;
            SetColor(_validColor);
        }

        public void SetState(bool isPointValid)
        {
            if (_isPointValid == isPointValid)
                return;

            _isPointValid = isPointValid;

            Color color = _isPointValid? _validColor : _invalidColor;

            SetColor(color);
        }

        private void SetColor(Color color) =>
            _renderer.material.SetColor(ColorPropertyName, color);
    }
}
