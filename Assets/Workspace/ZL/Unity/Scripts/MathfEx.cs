using System;

using System.Collections.Generic;

using Random = UnityEngine.Random;

namespace ZL.Unity
{
    public static partial class MathfEx
    {
        public static int RoundRandom(float f)
        {
            int integerPart = (int)f;

            float decimalPart = f - integerPart;

            if (decimalPart > 0f)
            {
                integerPart += Random.value < decimalPart ? 1 : 0;
            }

            return integerPart;
        }

        public static bool CDF<T>(IReadOnlyList<T> table, Func<T, float> GetWeight, out int result)
        {
            float cumulative = 0f;

            float maxThreshold = 0f;

            for (int i = 0; i < table.Count; ++i)
            {
                maxThreshold += GetWeight(table[i]);
            }

            float threshold = Random.Range(0f, maxThreshold);

            for (int i = 0; i < table.Count; ++i)
            {
                cumulative += GetWeight(table[i]);

                if (cumulative > threshold)
                {
                    result = i;

                    return true;
                }
            }

            result = -1;

            return false;
        }
    }
}