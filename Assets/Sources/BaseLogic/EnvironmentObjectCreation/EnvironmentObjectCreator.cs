using Assets.Sources.BaseLogic.EnvironmentObject;
using Assets.Sources.StaticDataService;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using Object = UnityEngine.Object;

namespace Assets.Sources.BaseLogic.EnvironmentObjectCreation
{
    public class EnvironmentObjectCreator
    {
        private readonly ReadOnlyArray<EnvironmentObjectType> _objectTypes;
        private readonly IStaticDataService _staticDataService;

        private int _currentObjectIndex;

        public EnvironmentObjectCreator(IStaticDataService staticDataService)
        {
            _objectTypes = staticDataService.EnvironmentOcjectsConfigurations.Select(configuration => configuration.Type).ToArray();
            _staticDataService = staticDataService;

            _currentObjectIndex = 0;
        }

        public event Action Created;

        public EnvironmentObject.EnvironmentObject CurrentObject { get; private set; }

        public void ChangeObject(bool isNextObject)
        {
            _currentObjectIndex += isNextObject ? 1 : -1;

            if (_currentObjectIndex < 0)
                _currentObjectIndex = _objectTypes.Count - 1;
            else if (_currentObjectIndex >= _objectTypes.Count)
                _currentObjectIndex = 0;

            Create();
        }

        public void Create()
        {
            if (CurrentObject != null)
                CurrentObject.Destroy();

            EnviromentObjectConfiguration configuration = _staticDataService.GetEnvironmentObjectConfiguration(_objectTypes[_currentObjectIndex]);

            CurrentObject = Object.Instantiate(configuration.Prefab);
            CurrentObject.Initialize(configuration.ColorPropertyName);

            Created?.Invoke();
        }
    }
}
