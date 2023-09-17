using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HL.Combat
{
    public class EnemyWaveSpawner : MonoBehaviour
    {
        [Header("Spawn Timer")]
        [SerializeField] float timeBetweenWaves = 30f;
        [SerializeField] float countdownTime = 30f;

        [Header("Enemy To Spawn")]
        [SerializeField] GameObject enemy;

        [Header("Spawn Locations")]
        [SerializeField] Transform spawn1;
        [SerializeField] Transform spawn2;
        [SerializeField] Transform spawn3;
        [SerializeField] Transform spawn4;
        [SerializeField] Transform spawn5;
        [SerializeField] Transform spawn6;

        void Start()
        {
            SpawnWave();
        }

        void Update()
        {
            SpawnTimer();
        }

        void SpawnTimer()
        {
            countdownTime -= Time.deltaTime;
            if(countdownTime <= 0)
            {
                countdownTime = timeBetweenWaves;
                SpawnWave();
            }
        }

        void SpawnWave()
        {
            Instantiate(enemy, spawn1.position, spawn1.rotation);
            Instantiate(enemy, spawn2.position, spawn2.rotation);
            Instantiate(enemy, spawn3.position, spawn3.rotation);
            Instantiate(enemy, spawn4.position, spawn4.rotation);
            Instantiate(enemy, spawn5.position, spawn5.rotation);
            Instantiate(enemy, spawn6.position, spawn6.rotation);
        }
    }
}
