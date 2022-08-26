using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IHS.UI
{
    public class SceneMenuManager : MonoBehaviour
    {
        [SerializeField] Scene scene;
        void Start()
        {
            
        }

        public void LoadLevel()
        {
            SceneManager.LoadScene(1);
        }

        public void Back()
        {
            SceneManager.LoadScene(0);
        }
    }

}