using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

namespace IHS.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static event Action GameOver;
        public static event Action PauseGame;
        public static event Action UnPauseGame;
        public static event Action LevelConditionsMet;
        public static event Action ScanForWormhole;
        public static event Action EnemyDestroyed;

        [SerializeField] KeyCode toggleKey = KeyCode.Escape;

        bool gameIsPaused;

        void Update()
        {
            if(Input.GetKeyDown(toggleKey))
            {
                TogglePauseMenu();
            }
        }

        public static void PauseGameEvent()
        {
            Time.timeScale = 0f;
            Cursor.visible = true;
            PauseGame?.Invoke();
        }

        public static void UnPauseGameEvent()
        {
            Time.timeScale = 1f;
            Cursor.visible = false;
            UnPauseGame?.Invoke();
        }

        public static void GameOverEvent()
        {
            Time.timeScale = 0f;
            Cursor.visible = true;
            GameOver?.Invoke();
        }

        public static void LevelConditionsMetEvent()
        {
            LevelConditionsMet?.Invoke();
        }

        public static void ScanForWormholeEvent()
        {
            ScanForWormhole?.Invoke();
        }

        public static void EnemyDestroyedEvent()
        {
            EnemyDestroyed?.Invoke();
        }

        public void TogglePauseMenu()
        {
            gameIsPaused = !gameIsPaused;

            if(gameIsPaused)
            {
                PauseGameEvent();
            }
            if(!gameIsPaused)
            {
                UnPauseGameEvent();
            }
        }

        public void LoadNextLevel()
        {
            Debug.Log("Load Called From Game Manager");
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;
            if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            {
                nextSceneIndex = 0;
            }
            SceneManager.LoadScene(nextSceneIndex);
        }

        public void ReloadLevel()
        {
            Time.timeScale = 1f;
            Cursor.visible = false;
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }

        public void ReturnToMainMenu()
        {
            Time.timeScale = 1f;
            Cursor.visible = true;
            SceneManager.LoadScene(0);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

    }
}
