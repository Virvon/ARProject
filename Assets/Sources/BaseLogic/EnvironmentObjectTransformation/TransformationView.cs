using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Sources.BaseLogic.EnvironmentObjectTransformation
{
    public class TransformationView : View
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Button _hideButton;
        [SerializeField] private Button _colorSelectionButton;
        [SerializeField] private Button _destroyButton;
        [SerializeField] private Button _animationButton;

        public event Action HideButtonClicked;
        public event Action ColorSelectionButtonClicked;
        public event Action DestroyButtonClicked;
        public event Action AnimationButtonClicked;

        public override void Show()
        {
            _hideButton.onClick.AddListener(OnHideButtonClicked);
            _colorSelectionButton.onClick.AddListener(OnColorSelectionButtonClicked);
            _destroyButton.onClick.AddListener(OnDestroyButtonClicked);
            _animationButton.onClick.AddListener(OnAnimationButtonClicked);

            _canvas.enabled = true;   
        }

        public override void Hide()
        {
            _hideButton.onClick.RemoveListener(OnHideButtonClicked);
            _colorSelectionButton.onClick.RemoveListener(OnColorSelectionButtonClicked);
            _destroyButton.onClick.RemoveListener(OnDestroyButtonClicked);
            _animationButton.onClick.RemoveListener(OnAnimationButtonClicked);

            SetAnimationButtonActive(false);

            _canvas.enabled = false;
        }

        private void OnHideButtonClicked() =>
            HideButtonClicked?.Invoke();

        private void OnColorSelectionButtonClicked() =>
            ColorSelectionButtonClicked?.Invoke();

        private void OnDestroyButtonClicked() =>
            DestroyButtonClicked?.Invoke();

        private void OnAnimationButtonClicked() =>
            AnimationButtonClicked?.Invoke();

        public void SetAnimationButtonActive(bool isActive) =>
            _animationButton.gameObject.SetActive(isActive);
    }
}
