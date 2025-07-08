using UnityEngine;

using ZL.Unity.IO;

using ZL.Unity.Singleton;

namespace ZL.Unity
{
    [AddComponentMenu("ZL/Application Manager (Singleton)")]

    public sealed class ApplicationManager : MonoSingleton<ApplicationManager>
    {
        [Space]

        [SerializeField]

        private BoolPref runInBackgroundPref = new("RunInBackground", false);

        public BoolPref RunInBackgroundPref
        {
            get => runInBackgroundPref;
        }

        [Space]

        [SerializeField]

        private IntPref targetFrameRatePref = new("TargetFrameRate", 60);

        public IntPref TargetFrameRatePref
        {
            get => targetFrameRatePref;
        }

        [Space]

        [Text("Cursor")]

        [AddIndent]

        [Alias("Visible")]

        [UsingCustomProperty]

        [SerializeField]

        private bool cursorVisible = true;

        [AddIndent]

        [Alias("Lock State")]

        [UsingCustomProperty]

        [SerializeField]

        private CursorLockMode cursorLockState = CursorLockMode.None;

        [Space]

        [PropertyField]

        [Button(nameof(Pause))]

        [Button(nameof(Resume))]

        [Button(nameof(Quit))]

        [UsingCustomProperty]

        [SerializeField]

        private float timeScale = 1f;

        private void OnValidate()
        {
            if (Application.isPlaying == false)
            {
                return;
            }

            Cursor.visible = cursorVisible;

            Cursor.lockState = cursorLockState;

            TimeEx.TimeScale = timeScale;
        }

        protected override void Awake()
        {
            base.Awake();

            runInBackgroundPref.OnValueChanged += (value) =>
            {
                Application.runInBackground = value;
            };

            runInBackgroundPref.TryLoadValue();

            targetFrameRatePref.OnValueChanged += (value) =>
            {
                Application.targetFrameRate = value;
            };

            targetFrameRatePref.TryLoadValue();

            Cursor.visible = cursorVisible;

            Cursor.lockState = cursorLockState;

            TimeEx.TimeScale = timeScale;
        }

        public void Pause()
        {
            TimeEx.Pause();

            timeScale = TimeEx.TimeScale;
        }

        public void Resume()
        {
            TimeEx.Resume();

            timeScale = TimeEx.TimeScale;
        }

        public void Quit()
        {
            FixedApplication.Quit();
        }
    }
}