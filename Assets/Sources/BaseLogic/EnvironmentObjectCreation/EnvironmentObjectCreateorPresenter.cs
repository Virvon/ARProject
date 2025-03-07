using System;

namespace Assets.Sources.BaseLogic.EnvironmentObjectCreation
{
    public class EnvironmentObjectCreateorPresenter : IDisposable
    {
        private readonly EnvironmentObjectCreationView _view;
        private readonly EnvironmentObjectCreator _model;

        public EnvironmentObjectCreateorPresenter(EnvironmentObjectCreationView view, EnvironmentObjectCreator model)
        {
            _view = view;
            _model = model;

            _view.ChangeButtonClicked += OnChangeButtonClicked;
        }

        public void Dispose()
        {
            _view.ChangeButtonClicked -= OnChangeButtonClicked;
        }

        private void OnChangeButtonClicked(bool isNextObject)
        {
            _model.ChangeObject(isNextObject);
        }
    }
}
