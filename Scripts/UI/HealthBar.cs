using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HL.Core;

namespace HL.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Health health = null;
        [SerializeField] RectTransform foreground = null;
        [SerializeField] Canvas rootCanvas = null;

        void Update()
        {
            if(Mathf.Approximately(health.GetFraction(), 0)
            || Mathf.Approximately(health.GetFraction(), 1))
            {
                rootCanvas.enabled = false;
                return;
            }
            rootCanvas.enabled = true;

            foreground.GetComponent<Image>().fillAmount = health.GetFraction();
        }
    }
}
