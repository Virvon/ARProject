using System;

namespace Assets.Sources.BaseLogic.EnvironmentObjectCreation
{
    public class CreatorPresenter : IDisposable
    {
        private readonly CreationView _view;
        private readonly CreationModel _model;

        public CreatorPresenter(CreationView view, CreationModel model)
        {
            _view = view;
            _model = model;

            _view.ChangeButtonClicked += OnChangeButtonClicked;
            _view.HideButtonClicked += OnHideButtonClicked;
        }

        public void Dispose()
        {
            _view.ChangeButtonClicked -= OnChangeButtonClicked;
            _view.HideButtonClicked -= OnHideButtonClicked;
        }

        private void OnChangeButtonClicked(bool isNextObject) =>
            _model.ChangeObject(isNextObject);

        private void OnHideButtonClicked() =>
            _model.MoveReviewState();
    }
}
