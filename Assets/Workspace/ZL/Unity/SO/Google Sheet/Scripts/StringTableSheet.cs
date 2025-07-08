using UnityEngine;

namespace ZL.Unity.SO.GoogleSheet
{
    [CreateAssetMenu(menuName = "ZL/SO/Google Sheet/String Table Sheet", fileName = "String Table Sheet")]

    public sealed class StringTableSheet : ScriptableGoogleSheet<string, StringTable>
    {
        protected override string GetDataKey(StringTable data)
        {
            return data.name;
        }
    }
}