using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Sources.BaseLogic
{
    public class ReviewView : View
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Button _creationButton;
        [SerializeField] private Button _informationButton;

        public event Action CreationButtonClicked;
        public event Action InformationButtonClicked;

        public override void Show()
        {
            _creationButton.onClick.AddListener(OnButtonClicked);
            _informationButton.onClick.AddListener(OnInformationButtonClicked);

            _canvas.enabled = true;
        }

        public override void Hide()
        {
            _creationButton.onClick.RemoveListener(OnButtonClicked);
            _informationButton.onClick.RemoveListener(OnInformationButtonClicked);

            _canvas.enabled = false;
        }

        private void OnButtonClicked() =>
            CreationButtonClicked?.Invoke();

        private void OnInformationButtonClicked() =>
            InformationButtonClicked?.Invoke();
    }
}
