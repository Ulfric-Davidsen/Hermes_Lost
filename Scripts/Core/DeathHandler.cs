using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FirstGearGames.SmoothCameraShaker;
using HL.Managers;

namespace HL.Core
{
    public class DeathHandler : MonoBehaviour
    {
        [Header("Death VFX")]
        [SerializeField] ParticleSystem deathParticles = null;
        [SerializeField] Transform particleSpawn = null;

        [Header("Death Delay Time")]
        [SerializeField] float deathDelay = 0.5f;

        [Header("Death Camera Shake")]
        public ShakeData MyShake;

        [Header("Player Mesh")]
        [SerializeField] GameObject playerMesh;

        Player player;
        Health health;

        bool isTransitioning = false;

        void Start()
        {
            player = GetComponent<Player>();
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

            playerMesh.SetActive(false);
            Instantiate(deathParticles, particleSpawn.position, particleSpawn.rotation);
            CameraShakerHandler.Shake(MyShake);
            StartCoroutine(GameOverCoroutine());

        }

        IEnumerator GameOverCoroutine()
        {
            yield return new WaitForSeconds(deathDelay);
            GameManager.GameOverEvent();
        }
    }
}
