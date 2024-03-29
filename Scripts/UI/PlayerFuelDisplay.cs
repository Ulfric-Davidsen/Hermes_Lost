using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IHS.Core;

namespace IHS.UI
{
    public class PlayerFuelDisplay : MonoBehaviour
    {
        [SerializeField] RectTransform foreground = null;

        Fuel fuel;

        void Start()
        {
            fuel = GameObject.FindWithTag("Player").GetComponent<Fuel>();
        }

        
        void Update()
        {
            if (foreground == null) return;

            foreground.GetComponent<Image>().fillAmount = fuel.GetFraction();
        }
    }
}
