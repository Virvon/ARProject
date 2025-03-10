using System;

namespace Assets.Sources.BaseLogic.EnvironmentObjectCreation
{
    public class CreatorPresenter : IDisposable
    {
        private readonly CreationView _view;
        private readonly EnvironmentObjectCreator _model;
        private readonly EnvironmentObjectCameraPositioner _cameraPositioner;

        private bool _isModelReseted;

        public CreatorPresenter(CreationView view, EnvironmentObjectCreator model, EnvironmentObjectCameraPositioner cameraPositioner)
        {
            _view = view;
            _model = model;
            _cameraPositioner = cameraPositioner;

            _isModelReseted = true;

            _view.ChangeButtonClicked += OnChangeButtonClicked;
            _model.Created += OnObjectCreated;
            _cameraPositioner.Completed += OnCompleted;
        }

        public void Dispose()
        {
            _view.ChangeButtonClicked -= OnChangeButtonClicked;
            _model.Created -= OnObjectCreated;
            _cameraPositioner.Completed -= OnCompleted;
        }

        private void OnChangeButtonClicked(bool isNextObject) =>
            _model.ChangeObject(isNextObject);

        private void OnCompleted()
        {
            _isModelReseted = true;
            _view.Hide();
        }

        private void OnObjectCreated()
        {
            if (_isModelReseted)
            {
                _view.Show();
                _isModelReseted = false;
            }
        }
    }
    public class EnvironmentObjectCreation
    {

    }
}
