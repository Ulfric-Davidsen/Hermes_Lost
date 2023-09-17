using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using HL.Managers;

namespace HL.Core
{
    public class SuccessHandler : MonoBehaviour
    {
        [Header("Success VFX")]
        [SerializeField] ParticleSystem successParticles;
        [SerializeField] Transform particleSpawn;

        [Header("Player Mesh")]
        [SerializeField] GameObject playerMesh;

        [Header("Time Before Load")]
        [SerializeField] float waitTime = 1f;

        Player player;

        bool isTransitioning = false;

        void Start()
        {
            player = GetComponent<Player>();
        }

        void OnTriggerEnter(Collider collision)
        {
            if (isTransitioning) {return;}

            if (collision.gameObject.tag == "Finish")
            {
                StartCoroutine("FinishSequence");
            }
        }

        private IEnumerator FinishSequence()
        {
            isTransitioning = true;

            player.enabled = false;
            playerMesh.SetActive(false);
            Instantiate(successParticles, particleSpawn.position, particleSpawn.rotation);
            yield return new WaitForSeconds(waitTime);
            GameManager.LoadNextLevel();
        }
    }
}
