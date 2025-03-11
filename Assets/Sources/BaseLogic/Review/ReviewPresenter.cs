using System;

namespace Assets.Sources.BaseLogic
{
    public class ReviewPresenter : IDisposable
    {
        private readonly ReviewModel _model;
        private readonly ReviewView _view;

        public ReviewPresenter(ReviewModel model, ReviewView view)
        {
            _model = model;
            _view = view;

            _view.CreationButtonClicked += OnCreationButtonClicked;
            _view.InformationButtonClicked += OnInformationButtonClicked;
        }

        public void Dispose()
        {
            _view.CreationButtonClicked -= OnCreationButtonClicked;
            _view.InformationButtonClicked -= OnInformationButtonClicked;
        }

        private void OnCreationButtonClicked() =>
            _model.MoveCreationState();

        private void OnInformationButtonClicked() =>
            _model.MoveInformationState();
    }
}
