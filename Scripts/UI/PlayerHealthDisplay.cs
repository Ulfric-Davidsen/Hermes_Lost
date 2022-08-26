using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IHS.Core;

namespace IHS.UI
{
    public class PlayerHealthDisplay : MonoBehaviour
    {
        [SerializeField] RectTransform foreground = null;

        Health health;

        void Start()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
        }

        
        void Update()
        {
            if (foreground == null) return;

            foreground.GetComponent<Image>().fillAmount = health.GetFraction();
        }
    }
}
