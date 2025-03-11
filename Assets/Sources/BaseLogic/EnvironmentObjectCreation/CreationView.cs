using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Sources.BaseLogic.EnvironmentObjectCreation
{
    public class CreationView : View
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Button _nextEnvironmentObjectButton;
        [SerializeField] private Button _previoustEnvironmentObjectButton;
        [SerializeField] private Button _hideButton;

        public event Action<bool> ChangeButtonClicked;
        public event Action HideButtonClicked;

        public override void Show()
        {
            _nextEnvironmentObjectButton.onClick.AddListener(OnNextEnvironmentObjectButtonClicked);
            _previoustEnvironmentObjectButton.onClick.AddListener(OnPreviousEnvironmentObjectButtonClicked);
            _hideButton.onClick.AddListener(OnHideButtonClicked);

            _canvas.enabled = true;
        }

        public override void Hide()
        {
            _nextEnvironmentObjectButton.onClick.RemoveListener(OnNextEnvironmentObjectButtonClicked);
            _previoustEnvironmentObjectButton.onClick.RemoveListener(OnPreviousEnvironmentObjectButtonClicked);
            _hideButton.onClick.RemoveListener(OnHideButtonClicked);

            _canvas.enabled = false;
        }

        private void OnPreviousEnvironmentObjectButtonClicked() =>
            ChangeButtonClicked?.Invoke(false);

        private void OnNextEnvironmentObjectButtonClicked() =>
            ChangeButtonClicked?.Invoke(true);

        private void OnHideButtonClicked() =>
            HideButtonClicked?.Invoke();
    }
}
