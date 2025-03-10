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
            _view.ColorSelectionButtonClicked += OnColorSelectionButtonClicked;
            _view.DestroyButtonClicked += OnDestroyButtonClicked;
        }

        public void Dispose()
        {
            _view.HideButtonClicked -= OnHideButtonClicked;
            _view.ColorSelectionButtonClicked -= OnColorSelectionButtonClicked;
            _view.DestroyButtonClicked -= OnDestroyButtonClicked;
        }

        private void OnHideButtonClicked() =>
            _model.MoveReviewState();

        private void OnColorSelectionButtonClicked() =>
            _model.MoveColorSelectionState();

        private void OnDestroyButtonClicked() =>
            _model.DestroyEnvironmentObject();
    }
}
