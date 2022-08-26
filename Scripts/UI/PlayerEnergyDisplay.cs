using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IHS.Core;

namespace IHS.UI
{
    public class PlayerEnergyDisplay : MonoBehaviour
    {
        [SerializeField] RectTransform foreground = null;

        Energy energy;

        void Start()
        {
            energy = GameObject.FindWithTag("Player").GetComponent<Energy>();
        }

        
        void Update()
        {
            if (foreground == null) return;

            foreground.GetComponent<Image>().fillAmount = energy.GetFraction();
        }
    }
}
