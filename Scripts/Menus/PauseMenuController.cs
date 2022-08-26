using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace IHS.Menus
{
    public class PauseMenuController : MonoBehaviour
    {
        [Header("Game Pausing Events")]
        [SerializeField] UnityEvent gamePaused;
        [SerializeField] UnityEvent gameUnPaused;

        [Space(10)]
        [SerializeField] KeyCode toggleKey = KeyCode.Escape;
        [SerializeField] GameObject pauseMenu = null;
        public static bool gameIsPaused;

        void Start()
        {
            pauseMenu.SetActive(false);
        }

        void Update()
        {
            if(Input.GetKeyDown(toggleKey))
            {
                TogglePauseMenu();
                gameIsPaused = !gameIsPaused;

                if (gameIsPaused)
                {
                    PauseGame();
                }

                if(!gameIsPaused)
                {
                    UnPauseGame();
                }
            }
        }

        public void TogglePauseMenu()
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
        }

        public void ResumeGame()
        {
            UnPauseGame();
            TogglePauseMenu();
        }

        public void ReturnToMainMenu()
        {
            UnPauseGame();
            SceneManager.LoadScene(0);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        void PauseGame()
        {
            if(gameIsPaused)
            {
                Time.timeScale = 0f;
                gamePaused.Invoke();
            }
            else
            {
                UnPauseGame();
            }
        }

        void UnPauseGame()
        {
            Time.timeScale = 1f;
            gameUnPaused.Invoke();
        }
    }
}
