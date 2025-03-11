using UnityEngine;

namespace Assets.Sources.BaseLogic.EnvironmentObject.ColorChanging
{
    public class PartsColorChanger : ColorChanger
    {
        [SerializeField] private ColorPart[] _colorParts;

        public override void Initialize(string colorPropertyName)
        {
            base.Initialize(colorPropertyName);

            foreach (ColorPart colorPart in _colorParts)
                colorPart.Initialize(ColorPropertyName);
        }

        protected override void ChangeColor()
        {
            foreach (ColorPart colorPart in _colorParts)
                colorPart.ChangeColor(Color, ColorPropertyName);
        }
    }
}
