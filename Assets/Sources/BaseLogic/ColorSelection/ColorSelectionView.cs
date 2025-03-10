using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Sources.BaseLogic.ColorSelection
{
    public class ColorSelectionView : View
    {
        [SerializeField] private Scrollbar _redScrollbar;
        [SerializeField] private Scrollbar _greenScrollbar;
        [SerializeField] private Scrollbar _blueScrollbar;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Button _hideButton;

        public event Action<Color> ColorChanged;
        public event Action HideButtonClicked;

        public override void Show()
        {
            _redScrollbar.onValueChanged.AddListener(OnColorChanged);
            _greenScrollbar.onValueChanged.AddListener(OnColorChanged);
            _blueScrollbar.onValueChanged.AddListener(OnColorChanged);
            _hideButton.onClick.AddListener(OnHideButtonClicked);

            _canvas.enabled = true;
        }

        public override void Hide()
        {
            _redScrollbar.onValueChanged.RemoveListener(OnColorChanged);
            _greenScrollbar.onValueChanged.RemoveListener(OnColorChanged);
            _blueScrollbar.onValueChanged.RemoveListener(OnColorChanged);
            _hideButton.onClick.RemoveListener(OnHideButtonClicked);

            _canvas.enabled = false;
        }

        public void SetColor(Color color)
        {
            _redScrollbar.value = color.r;
            _greenScrollbar.value = color.g;
            _blueScrollbar.value = color.b;
        }

        private void OnColorChanged(float value) =>
            ColorChanged?.Invoke(new Color(_redScrollbar.value, _greenScrollbar.value, _blueScrollbar.value));

        private void OnHideButtonClicked() =>
            HideButtonClicked?.Invoke();
    }
}
