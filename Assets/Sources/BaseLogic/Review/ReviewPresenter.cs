using System;

namespace Assets.Sources.BaseLogic
{
    public class ReviewPresenter : IDisposable
    {
        private readonly Review _model;
        private readonly ReviewView _view;

        public ReviewPresenter(Review model, ReviewView view)
        {
            _model = model;
            _view = view;

            _view.ButtonClicked += OnButtonClicked;
        }

        public void Dispose() =>
            _view.ButtonClicked -= OnButtonClicked;

        private void OnButtonClicked() =>
            _model.MoveNextState();
    }
}
