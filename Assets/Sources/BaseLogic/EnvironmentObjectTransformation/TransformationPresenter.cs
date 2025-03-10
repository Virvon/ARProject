using System;

namespace Assets.Sources.BaseLogic.EnvironmentObjectTransformation
{
    public class TransformationPresenter : IDisposable
    {
        private readonly TransformationModel _model;
        private readonly TransformationView _view;

        public TransformationPresenter(TransformationModel transformationModel, TransformationView transformationView)
        {
            _model = transformationModel;
            _view = transformationView;

            _view.HideButtonClicked += OnHideButtonClicked;
        }

        public void Dispose()
        {
            _view.HideButtonClicked -= OnHideButtonClicked;
        }

        private void OnHideButtonClicked()
        {
            _model.MoveNextState();
        }
    }
}
