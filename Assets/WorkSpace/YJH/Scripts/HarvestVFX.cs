using System.Collections;

using UnityEngine;

using ZL.Unity.Coroutines;

using ZL.Unity.Pooling;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Harvest VFX")]

    public sealed class HarvestVFX : PooledObject
    {
        public void Play(Transform harvester)
        {
            gameObject.SetActive(true);

            StartCoroutine(Routine(harvester));
        }

        private IEnumerator Routine(Transform harvester)
        {
            yield return WaitForSecondsCache.Get(0.5f);

            while (true)
            {
                yield return WaitForEndOfFrameCache.Get();

                transform.position = harvester.position;
            }
        }
    }
}