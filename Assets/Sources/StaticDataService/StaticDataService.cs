using Assets.Sources.BaseLogic.EnvironmentObjectCreation;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

namespace Assets.Sources.StaticDataService
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<EnvironmentObjectType, EnviromentObjectConfiguration> _environmentObjectsConfigurations;

        public ReadOnlyArray<EnviromentObjectConfiguration> EnvironmentOcjectsConfigurations => _environmentObjectsConfigurations.Values.ToArray();

        public void Initialize()
        {
            _environmentObjectsConfigurations = Resources.LoadAll<EnviromentObjectConfiguration>(ConfigurationPath.EnvironmentObjects).ToDictionary(value => value.Type, value => value);
        }

        public EnviromentObjectConfiguration GetEnvironmentObject(EnvironmentObjectType type) =>
            _environmentObjectsConfigurations.TryGetValue(type, out EnviromentObjectConfiguration configuration) ? configuration : null;
    }
    [CreateAssetMenu(fileName = "EnviromentObjectConfiguration", menuName = "Configuration/Create new enviroment object configuration", order = 51)]
    public class EnviromentObjectConfiguration : ScriptableObject
    {
        public EnvironmentObjectType Type;
        public EnvironmentObject Prefab;
    }
    public class ConfigurationPath
    {
        public const string EnvironmentObjects = "";
    }
}
