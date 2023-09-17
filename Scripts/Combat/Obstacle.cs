using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FirstGearGames.SmoothCameraShaker;
using HL.Core;
using HL.Items;

namespace HL.Combat
{
    public class Obstacle : MonoBehaviour
    {
        [Header("Death VFX")]
        [SerializeField] ParticleSystem deathParticles = null;
        [SerializeField] Transform particleSpawn = null;

        [Header("Death Camera Shake")]
        public ShakeData MyShake;

        Health health;
        RandomItemSpawner randomItemSpawner;

        bool isTransitioning = false;

        void Start()
        {
            health = GetComponentInChildren<Health>();
            randomItemSpawner = GetComponent<RandomItemSpawner>();
        }

        void Update()
        {
            if (isTransitioning) {return;}

            if (health.IsDead())
            {
                randomItemSpawner.DropItem();
                Die();
            }
        }

        void Die()
        {
            isTransitioning = true;

            Instantiate(deathParticles, particleSpawn.position, particleSpawn.rotation);
            CameraShakerHandler.Shake(MyShake);
            Destroy(gameObject);
        }
    }
}
