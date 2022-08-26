using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace IHS.Core
{
    public class SuccessHandler : MonoBehaviour
    {
        [Header("Success VFX")]
        [SerializeField] ParticleSystem successParticles;
        [SerializeField] Transform particleSpawn;
        
        [Header("Load Next Level Delay")]
        [SerializeField] float timeDelay = 1f;

        [Header("Player Mesh")]
        [SerializeField] GameObject playerMesh;

        ShipMovement shipMovement;

        bool isTransitioning = false;

        void Start()
        {
            shipMovement = GetComponent<ShipMovement>();
        }

        void OnTriggerEnter(Collider collision)
        {
            if (isTransitioning) {return;}

            if (collision.gameObject.tag == "Finish")
            {
                FinishSequence();
            }
        }

        void FinishSequence()
        {
            isTransitioning = true;

            shipMovement.enabled = false;
            playerMesh.SetActive(false);
            Instantiate(successParticles, particleSpawn.position, particleSpawn.rotation);
            // GameManager.Instance.LoadNextLevel();
            Invoke ("LoadNextLevel", timeDelay);
        }

        void LoadNextLevel()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;
            if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            {
                nextSceneIndex = 0;
            }
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
