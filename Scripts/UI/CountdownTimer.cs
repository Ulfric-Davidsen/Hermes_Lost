using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HL.Managers;
using TMPro;

namespace HL.UI
{
    public class CountdownTimer : MonoBehaviour
    {
        [SerializeField] float timeRemaining = 120f;
        [SerializeField] TMP_Text countdownText;

        void Update()
        {
            countdownText.text = timeRemaining.ToString("0");

            if(timeRemaining > 0f)
            {
                CountDownTime();
            }
            return;
        }

        void CountDownTime()
        {
            timeRemaining -= Time.deltaTime;

            if(timeRemaining <= 0f)
            {
                TriggerLevelConditionsMet();
            }
        }

        void TriggerLevelConditionsMet()
        {
            GameManager.LevelConditionsMetEvent();
        }
    }
}
