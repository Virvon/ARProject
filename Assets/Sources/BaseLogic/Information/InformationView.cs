using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Sources.BaseLogic.Information
{
    public class InformationView : View
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Button _hideButton;

        public event Action HideButtonClicked;

        public override void Show()
        {
            _hideButton.onClick.AddListener(OnHideButtonClicked);

            _canvas.enabled = true;
        }

        public override void Hide()
        {
            _hideButton.onClick.RemoveListener(OnHideButtonClicked);

            _canvas.enabled = false;
        }

        private void OnHideButtonClicked() =>
            HideButtonClicked?.Invoke();
    }
}
