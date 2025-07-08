using System;

using UnityEngine;

using ZL.Unity.IO;

using ZL.Unity.Singleton;

namespace ZL.Unity.SO.GoogleSheet
{
    [AddComponentMenu("ZL/SO/Google Sheet/String Table Manager (Singleton)")]

    public sealed class StringTableManager : MonoSingleton<StringTableManager>
    {
        [Space]

        [Button(nameof(LoadLanguage))]

        [Button(nameof(SaveLanguage))]

        [Margin]

        [UsingCustomProperty]

        [SerializeField]

        private EnumPref<StringTableLanguage> targetLanguagePref = new("Target Language", StringTableLanguage.Korean);

        public StringTableLanguage TargetLanguage
        {
            get => targetLanguagePref.Value;

            set => targetLanguagePref.Value = value;
        }

        public event Action OnLanguageChangedAction = null;

        private void OnValidate()
        {
            targetLanguagePref.Value = targetLanguagePref.Value;
        }

        protected override void Awake()
        {
            base.Awake();

            targetLanguagePref.OnValueChanged += (value) =>
            {
                OnLanguageChangedAction?.Invoke();
            };

            LoadLanguage();
        }

        public void LoadLanguage()
        {
            targetLanguagePref.TryLoadValue();
        }

        public void SaveLanguage()
        {
            targetLanguagePref.SaveValue();
        }
    }
}