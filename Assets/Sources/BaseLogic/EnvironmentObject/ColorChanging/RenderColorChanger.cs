using UnityEngine;

namespace Assets.Sources.BaseLogic.EnvironmentObject.ColorChanging
{
    public class RenderColorChanger : ColorChanger
    {
        [SerializeField] private Renderer _renderer;

        protected override void ChangeColor()
        {
            Material[] materials = _renderer.materials;

            foreach (Material material in materials)
                material.SetColor(ColorPropertyName, Color);

            _renderer.materials = materials;
        }
    }
}
