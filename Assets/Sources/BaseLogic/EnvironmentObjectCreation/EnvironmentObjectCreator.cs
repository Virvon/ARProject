using Assets.Sources.StaticDataService;
using System;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Assets.Sources.BaseLogic.EnvironmentObjectCreation
{
    public class EnvironmentObjectCreator
    {
        private readonly ReadOnlyArray<EnvironmentObjectType> _objectTypes;
        private readonly IStaticDataService _staticDataService;

        private EnvironmentObjectType _currentType;
        private EnvironmentObject _currentObject;

        public EnvironmentObjectCreator(ReadOnlyArray<EnvironmentObjectType> objectTypes, IStaticDataService staticDataService)
        {
            _objectTypes = objectTypes;
            _staticDataService = staticDataService;

            _currentType = objectTypes[0];

            Create();
        }

        public void ChangeObject(bool isNextObject)
        {
            throw new NotImplementedException();
        }

        private void Create()
        {
            if (_currentObject != null)
                Object.Destroy(_currentObject);

            _currentObject = Object.Instantiate(_staticDataService.GetEnvironmentObject(_currentType).Prefab);
        }
    }
    public class CreatingEnvironmentObjectPositioner
    {

    }
    public class EnvironmentObject : MonoBehaviour
    {

    }
    public enum EnvironmentObjectType
    {
        Chair,
    }
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
    public class EnvironmentObjectCreationView : View
    {
        [SerializeField] private Button _nextEnvironmentObjectButton;
        [SerializeField] private Button _previoustEnvironmentObjectButton;

        public event Action<bool> ChangeButtonClicked;

        public override void Show()
        {
            _nextEnvironmentObjectButton.onClick.AddListener(OnNextEnvironmentObjectButtonClicked);
            _previoustEnvironmentObjectButton.onClick.AddListener(OnPreviousEnvironmentObjectButtonClicked);
        }

        public override void Hide()
        {
            _nextEnvironmentObjectButton.onClick.RemoveListener(OnNextEnvironmentObjectButtonClicked);
            _previoustEnvironmentObjectButton.onClick.RemoveListener(OnPreviousEnvironmentObjectButtonClicked);
        }

        private void OnPreviousEnvironmentObjectButtonClicked() =>
            ChangeButtonClicked?.Invoke(false);

        private void OnNextEnvironmentObjectButtonClicked() =>
            ChangeButtonClicked?.Invoke(true);
    }
    public abstract class View : MonoBehaviour
    {
        public abstract void Show();
        public abstract void Hide();
    }
}
