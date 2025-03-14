﻿using Assets.Sources.BaseLogic.EnvironmentObject;
using UnityEngine;

namespace Assets.Sources.StaticDataService
{
    [CreateAssetMenu(fileName = "EnviromentObjectConfiguration", menuName = "Configuration/Create new enviroment object configuration", order = 51)]
    public class EnviromentObjectConfiguration : ScriptableObject
    {
        public EnvironmentObjectType Type;
        public EnvironmentObject Prefab;
        public string ColorPropertyName;
    }
}
