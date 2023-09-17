using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HL.UI
{
    public class FuelAnimationTrigger : MonoBehaviour
    {
        public void SetBoolTrue()
        {
            GetComponent<Animator>().SetBool("lowFuel", true);
        }

        public void SetBoolFalse()
        {
            GetComponent<Animator>().SetBool("lowFuel", false);
        }
    }
}
