using JDG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JDG
{
    public enum ResourcesType
    {
        None, IngameCurrency, MetaCurrency, Blueprint
    }

    [System.Serializable]
    public class ResourceCost
    {
        public ResourcesType _resourceType;
        public int _value;
    }
}
