using UnityEngine;

namespace Assets.Sources.BaseLogic.EnvironmentObject.ColorChanging
{
    public class ColorPart : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;

        private Color[] _colors;

        public void Initialize(string colorPropertyName)
        {
            _colors = new Color[_renderer.materials.Length];

            for (int i = 0; i < _renderer.materials.Length; i++)
                _colors[i] = _renderer.materials[i].GetColor(colorPropertyName);
        }

        public void ChangeColor(Color color, string colorPropertyName)
        {
            Material[] materials = _renderer.materials;

            for (int i = 0; i < materials.Length; i++)
                materials[i].SetColor(colorPropertyName, _colors[i] * color);

            _renderer.materials = materials;
        }
    }
}
