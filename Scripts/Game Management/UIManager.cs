using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IHS.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] GameObject gameOverMenu = null;
        [SerializeField] GameObject pauseMenu = null;
        
        void Start()
        {
            GameManager.GameOver += SetGameOverMenuActive;
            GameManager.PauseGame += SetPauseMenuActive;
            GameManager.UnPauseGame += SetPauseMenuInactive;
        }

        void SetGameOverMenuActive()
        {
            gameOverMenu.SetActive(true);
        }

        void SetPauseMenuActive()
        {
            pauseMenu.SetActive(true);
        }

        void SetPauseMenuInactive()
        {
            pauseMenu.SetActive(false);
        }

        void OnDisable()
        {
            GameManager.GameOver -= SetGameOverMenuActive;
            GameManager.PauseGame -= SetPauseMenuActive;
            GameManager.UnPauseGame -= SetPauseMenuInactive;
        }
    }
}
