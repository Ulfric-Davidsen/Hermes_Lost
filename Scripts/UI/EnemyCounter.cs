using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HL.Managers;
using TMPro;

namespace HL.UI
{
    public class EnemyCounter : MonoBehaviour
    {
        [SerializeField] int enemiesToDestroy = 5;
        [SerializeField] TMP_Text enemyCounterText;

        void OnEnable()
        {
            GameManager.EnemyDestroyed += DecreaseEnemyCount;
        }
        
        void Update()
        {
            enemyCounterText.text = enemiesToDestroy.ToString("0");
        }

        void DecreaseEnemyCount()
        {
            enemiesToDestroy--;

            if(enemiesToDestroy <= 0)
            {
                TriggerLevelConditionsMet();
            }
        }

        void TriggerLevelConditionsMet()
        {
            GameManager.LevelConditionsMetEvent();
        }

        private void OnDisable()
        {
            GameManager.EnemyDestroyed -= DecreaseEnemyCount;
        }
    }
}
