using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FirstGearGames.SmoothCameraShaker;
using HL.Core;
using HL.Managers;

namespace HL.AI
{
    public class AIDeathHandler : MonoBehaviour
    {
        [Header("Death VFX")]
        [SerializeField] ParticleSystem deathParticles = null;
        [SerializeField] Transform particleSpawn = null;

        [Header("Death Camera Shake")]
        public ShakeData MyShake;

        [Header("Death Meets Level Conditions")]
        [SerializeField] bool meetsLevelConditions = false;

        Health health;

        bool isTransitioning = false;

        void Start()
        {
            health = GetComponent<Health>();
        }

        void Update()
        {
            if (isTransitioning) {return;}

            if (health.IsDead())
            {
                Die();
            }
        }

        void Die()
        {
            isTransitioning = true;

            Instantiate(deathParticles, particleSpawn.position, particleSpawn.rotation);
            CameraShakerHandler.Shake(MyShake);

            if(meetsLevelConditions)
            {
                GameManager.LevelConditionsMetEvent();
            }
            else
            {
                GameManager.EnemyDestroyedEvent();
            }
            
            Destroy(gameObject);
        }
    }
}
