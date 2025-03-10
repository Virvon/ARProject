using System;

namespace Assets.Sources.BaseLogic.EnvironmentObjectCreation
{
    public class CreatorPresenter : IDisposable
    {
        private readonly CreationView _view;
        private readonly EnvironmentObjectCreator _model;

        public CreatorPresenter(CreationView view, EnvironmentObjectCreator model)
        {
            _view = view;
            _model = model;

            _view.ChangeButtonClicked += OnChangeButtonClicked;
        }

        public void Dispose()
        {
            _view.ChangeButtonClicked -= OnChangeButtonClicked;
        }

        private void OnChangeButtonClicked(bool isNextObject) =>
            _model.ChangeObject(isNextObject);
    }
}
