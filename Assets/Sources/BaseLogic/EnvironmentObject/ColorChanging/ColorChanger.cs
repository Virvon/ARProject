using UnityEngine;

namespace Assets.Sources.BaseLogic.EnvironmentObject.ColorChanging
{
    public abstract class ColorChanger : MonoBehaviour
    {
        public Color Color { get; private set; }

        protected string ColorPropertyName { get; private set; }

        private void Start() =>
            Color = new Color(1, 1, 1);

        public virtual void Initialize(string colorPropertyName) =>
            ColorPropertyName = colorPropertyName;

        public void SetColor(Color color)
        {
            Color = color;

            ChangeColor();
        }

        protected abstract void ChangeColor();
    }
}
