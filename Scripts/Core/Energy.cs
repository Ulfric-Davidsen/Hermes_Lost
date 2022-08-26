using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IHS.Core
{
    public class Energy : MonoBehaviour
    {
        [Header("Energy Stats")]
        [SerializeField] float maxEnergy = 100f;
        [SerializeField] float energyAmount = 100f;
        [SerializeField] float energySpendAmount = 10f;
        [SerializeField] float energyRechargeRate = 1f;

        bool hasEnergy = true;

        ShipMovement shipMovement;

        void Start()
        {
            shipMovement = GetComponent<ShipMovement>();
        }

        void Update() 
        {
            CheckHasEnergy();
            if (shipMovement.IsFiring() && hasEnergy)
            {
                SpendEnergy();
            }
            if (!shipMovement.IsFiring() && energyAmount < maxEnergy)
            {
                RechargeEnergy();
            }
        }

        void SpendEnergy()
        {
            energyAmount -= energySpendAmount;
        }

        void RechargeEnergy()
        {
            energyAmount += energyRechargeRate * Time.deltaTime;
        }

        public float GetFraction()
        {
            return energyAmount / maxEnergy;
        }

        void CheckHasEnergy()
        {
            if (energyAmount >= energySpendAmount)
            {
                hasEnergy = true;
            }
            else
            {
                hasEnergy = false;
            }
        }

        public bool HasEnergy()
        {
            return hasEnergy;
        }

        
    }
}
