using UnityEngine;

namespace ZL.Unity.SO
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/Image Table", fileName = "Image Table")]

    public sealed class ImageTable : ScriptableDictionary<string, Sprite>
    {
        protected override string GetDataKey(Sprite data)
        {
            return data.name;
        }
    }
}