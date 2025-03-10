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
            _model.ActiveChanged += OnModelActiveChanged;
        }

        public void Dispose()
        {
            _view.ButtonClicked -= OnButtonClicked;
            _model.ActiveChanged -= OnModelActiveChanged;
        }

        private void OnModelActiveChanged(bool isActive)
        {
            if (isActive)
                _view.Show();
            else
                _view.Hide();
        }

        private void OnButtonClicked() =>
            _model.MoveNextState();
    }
}
