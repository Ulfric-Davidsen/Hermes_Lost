using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using IHS.Core;

namespace IHS.Menus
{
    public class GameOverController : MonoBehaviour
    {
        [SerializeField] GameObject gameOverController = null;

        void Start()
        {
            gameOverController.SetActive(false);
        }

        public void GameIsOver()
        {
            gameOverController.SetActive(true);
        }

        public void ReturnToMainMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void ReloadLevel()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

    }
}
