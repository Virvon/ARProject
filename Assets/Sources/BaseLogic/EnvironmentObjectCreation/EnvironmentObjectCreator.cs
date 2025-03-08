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

        private EnvironmentObjectType _currentType;

        public EnvironmentObjectCreator(IStaticDataService staticDataService)
        {
            _objectTypes = staticDataService.EnvironmentOcjectsConfigurations.Select(configuration => configuration.Type).ToArray();
            _staticDataService = staticDataService;

            _currentType = _objectTypes[0];
        }

        public EnvironmentObject CurrentObject { get; private set; }

        public void ChangeObject(bool isNextObject)
        {
            throw new NotImplementedException();
        }

        public void Create()
        {
            Debug.Log("Created");

            if (CurrentObject != null)
                Object.Destroy(CurrentObject);

            CurrentObject = Object.Instantiate(_staticDataService.GetEnvironmentObject(_currentType).Prefab);
        }

        public void Reset() =>
            CurrentObject = null;
    }
}
