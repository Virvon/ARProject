using Assets.Sources.BaseLogic.EnvironmentObject;
using UnityEngine.InputSystem.Utilities;

namespace Assets.Sources.StaticDataService
{
    public interface IStaticDataService
    {
        ReadOnlyArray<EnviromentObjectConfiguration> EnvironmentOcjectsConfigurations { get; }

        EnviromentObjectConfiguration GetEnvironmentObjectConfiguration(EnvironmentObjectType type);

        void Initialize();
    }
}