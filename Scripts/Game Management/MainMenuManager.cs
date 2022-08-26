using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace IHS.Managers
{
    public class MainMenuManager : MonoBehaviour
    {
        void Start()
        {
            Cursor.visible = true;
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

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
