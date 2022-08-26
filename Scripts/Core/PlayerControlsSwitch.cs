using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IHS.Managers;

namespace IHS.Core
{
    public class PlayerControlsSwitch : MonoBehaviour
    {
        ShipMovement shipMovement;

        void Start()
        {
            GameManager.PauseGame += ShipControlsOff;
            GameManager.UnPauseGame += ShipControlsOn;
            GameManager.GameOver += ShipControlsOff;

            shipMovement = GetComponent<ShipMovement>();
        }

        void ShipControlsOn()
        {
            shipMovement.enabled = true;
        }

        void ShipControlsOff()
        {
            shipMovement.enabled = false;
        }

        void OnDisable()
        {
            GameManager.PauseGame -= ShipControlsOff;
            GameManager.UnPauseGame -= ShipControlsOn;
            GameManager.GameOver -= ShipControlsOff;
        }
    }
}
