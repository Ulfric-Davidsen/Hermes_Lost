using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IHS.Managers;
using TMPro;

namespace IHS.UI
{
    public class EnemyCounter : MonoBehaviour
    {
        [SerializeField] int enemiesToDestroy = 5;
        [SerializeField] TMP_Text enemyCounterText;

        void Start()
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
