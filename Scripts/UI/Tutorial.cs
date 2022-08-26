using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IHS.UI
{
    public class Tutorial : MonoBehaviour
    {
        [SerializeField] GameObject playerPrefab = null;

        void Start()
        {
            Time.timeScale = 0f;
            Cursor.visible = true;
        }

        public void OnTutorialClose()
        {
            playerPrefab.SetActive(true);
            Cursor.visible = false;
            Time.timeScale = 1f;
        }
    }
}
