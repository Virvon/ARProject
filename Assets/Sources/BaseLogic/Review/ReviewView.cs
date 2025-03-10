using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Sources.BaseLogic
{
    public class ReviewView : View
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Button _button;

        public event Action ButtonClicked;

        public override void Show()
        {
            _button.onClick.AddListener(OnButtonClicked);
            _canvas.enabled = true;
            Debug.Log("Show review");
        }

        public override void Hide()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
            _canvas.enabled = false;
        }

        private void OnButtonClicked() =>
            ButtonClicked?.Invoke();
    }
}
