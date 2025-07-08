using UnityEngine;

namespace ZL.Unity
{
    public static partial class AnimationCurveEx
    {
        public static float LerpTime(this AnimationCurve instance)
        {
            return instance.keys[^1].time;
        }
    }
}