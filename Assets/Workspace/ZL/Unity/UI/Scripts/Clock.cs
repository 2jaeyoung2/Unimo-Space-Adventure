using System;

using System.Collections;

using UnityEngine;

using ZL.Unity.Coroutines;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Clock")]

    public class Clock : MonoBehaviour
    {
        [Space]

        [Alias("Time Stamp Text (UI)")]

        [UsingCustomProperty]

        [SerializeField]

        private TextController timeStampTextUI = null;

        [Space]

        [SerializeField]

        private int hour = 0;

        public int Hour
        {
            get => hour;

            set
            {
                hour = value;

                Refresh();
            }
        }

        [SerializeField]

        private int minute = 0;

        public int Minute
        {
            get => minute;

            set
            {
                minute = value;

                Refresh();
            }
        }

        [SerializeField]

        private int seconds = 0;

        public int Seconds
        {
            get => seconds;

            set
            {
                seconds = value;

                Refresh();
            }
        }

        [Space]

        [SerializeField]

        private float timeSpeed = 0f;

        [Space]

        [SerializeField]

        private bool isBlinking = true;

        public bool IsBlinking
        {
            set
            {
                isBlinking = value;

                if (isBlinking == true)
                {
                    StartBlinking();
                }

                else
                {
                    StopBlinking();
                }
            }
        }

        [ToggleIf(nameof(isBlinking), false)]

        [UsingCustomProperty]

        [SerializeField]

        private bool syncBlinking = false;

        [Space]

        [Tooltip("{0} = Hour\n{1} = Minute\n{2} = Seconds")]

        [SerializeField]

        private string timeStampFormat = "{0:D2}:{1:D2}:{2:D2}";

        [ToggleIf(nameof(isBlinking), false)]

        [Alias("Time Stamp Format (Blinked)")]

        [UsingCustomProperty]

        [SerializeField]

        private string timeStampFormat_Blinked = "{0:D2} {1:D2} {2:D2}";

        private bool isBlinked = false;

        private TimeSpan timeSpan = TimeSpan.Zero;

        private void OnValidate()
        {
            timeSpan = new TimeSpan(hour, minute, seconds);

            Refresh();
        }

        private void OnEnable()
        {
            IsBlinking = isBlinking;
        }

        private void Update()
        {
            timeSpan += TimeSpan.FromSeconds(timeSpeed * Time.deltaTime);

            Refresh();
        }

        public void Refresh()
        {
            hour = timeSpan.Hours;

            minute = timeSpan.Minutes;

            seconds = timeSpan.Seconds;

            if (timeStampTextUI == null)
            {
                return;
            }

            if (isBlinked == false)
            {
                timeStampTextUI.text = string.Format(timeStampFormat, hour, minute, (int)seconds);
            }

            else
            {
                timeStampTextUI.text = string.Format(timeStampFormat_Blinked, hour, minute, (int)seconds);
            }
        }

        public string GetTimeStamp()
        {
            return string.Format(timeStampFormat, hour, minute, seconds);
        }

        public void StartBlinking()
        {
            if (blinkingRoutine != null)
            {
                return;
            }

            blinkingRoutine = BlinkingRoutine();

            StartCoroutine(blinkingRoutine);
        }

        public void StopBlinking()
        {
            if (blinkingRoutine == null)
            {
                return;
            }

            StopCoroutine(blinkingRoutine);

            blinkingRoutine = null;
        }

        private IEnumerator blinkingRoutine = null;

        private IEnumerator BlinkingRoutine()
        {
            while (true)
            {
                if (syncBlinking == true)
                {
                    if (timeSpan.Milliseconds < 500)
                    {
                        isBlinked = false;
                    }

                    else
                    {
                        isBlinked = true;
                    }

                    yield return null;
                }

                else
                {
                    isBlinked = false;

                    yield return WaitForSecondsCache.Get(0.5f);

                    isBlinked = true;

                    yield return WaitForSecondsCache.Get(0.5f);
                }
            }
        }
    }
}