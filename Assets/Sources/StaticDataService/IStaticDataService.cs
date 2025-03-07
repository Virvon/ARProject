using Assets.Sources.BaseLogic.EnvironmentObjectCreation;
using UnityEngine.InputSystem.Utilities;

namespace Assets.Sources.StaticDataService
{
    public interface IStaticDataService
    {
        ReadOnlyArray<EnviromentObjectConfiguration> EnvironmentOcjectsConfigurations { get; }

        EnviromentObjectConfiguration GetEnvironmentObject(EnvironmentObjectType type);
        void Initialize();
    }
}