using System.Collections;
using System.Collections.Generic;
using HL.Managers;
using UnityEngine;

namespace HL.UI
{
    public class Tutorial : MonoBehaviour
    {
        [SerializeField] GameObject tutorialCanvas = null;
        [SerializeField] float waitTime = 1f;

        void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            StartCoroutine("TimeBeforeShowTutorial");
        }

        public void OnTutorialClose()
        {
            tutorialCanvas.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            GameManager.PlayerControlsOnEvent();
            Time.timeScale = 1f;
        }

        private IEnumerator TimeBeforeShowTutorial()
        {
            yield return new WaitForSeconds(waitTime);
            tutorialCanvas.SetActive(true);
            GameManager.PlayerControlsOffEvent();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
        }
    }
}
