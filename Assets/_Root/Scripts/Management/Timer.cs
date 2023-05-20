using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace ToyMatch
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] TMP_Text timeText;
        [SerializeField] UnityEvent dingDing;
        float _endTime;

        WaitUntil _waitToDingDing;

        bool TimetoDingDing()
        {
            return Time.time > _endTime;  
        }
        
        void Awake()
        {
            _waitToDingDing = new(TimetoDingDing);
        }
        
        public void SetTimer(float time)
        {
            StartCoroutine(StartTimer(time));
            StartCoroutine(UpdateTimer());
        }

        public void ExtendTime(float extension)
        {
            _endTime += extension;
        }

        IEnumerator StartTimer(float time)
        {
            _endTime = Time.time + time;
            yield return _waitToDingDing;
            dingDing.Invoke();
        }

        IEnumerator UpdateTimer()
        {
            while (!TimetoDingDing())
            {
                timeText.SetText(GetTimeLeft());
                yield return null;
            }
            timeText.SetText("0:00");
        }

        string GetTimeLeft()
        {
            return TimeSpan.FromSeconds(_endTime - Time.time).ToString("%m\\:ss");
        }
    }
}
