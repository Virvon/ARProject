using System;

namespace Assets.Sources.BaseLogic.Information
{
    public class InformationPresenter : IDisposable
    {
        private readonly InformationModel _model;
        private readonly InformationView _view;

        public InformationPresenter(InformationModel informationModel, InformationView informationView)
        {
            _model = informationModel;
            _view = informationView;

            _view.HideButtonClicked += OnHideButtonClicked;
        }

        public void Dispose() =>
            _view.HideButtonClicked -= OnHideButtonClicked;

        private void OnHideButtonClicked() =>
            _model.MoveNextState();
    }
}
