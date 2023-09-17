using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HL.Managers;

namespace HL.Core
{
    public class PlayerControlsSwitch : MonoBehaviour
    {
        Player player;

        void OnEnable()
        {
            GameManager.PlayerControlsOff += PlayerControlsOff;
            GameManager.PlayerControlsOn += PlayerControlsOn;
        }
        void Start()
        {
            player = GetComponent<Player>();
        }

        void PlayerControlsOn()
        {
            player.enabled = true;
        }

        void PlayerControlsOff()
        {
            player.enabled = false;
        }

        void OnDisable()
        {
            GameManager.PlayerControlsOff -= PlayerControlsOff;
            GameManager.PlayerControlsOn -= PlayerControlsOn;
        }
    }
}
