using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace IHS.Core
{
    public class Fuel : MonoBehaviour
    {
        [Header("Fuel Events")]
        [SerializeField] UnityEvent lowFuel;
        [SerializeField] UnityEvent fuelAboveLow;
        [SerializeField] UnityEvent outOfFuel;
        

        [Header("Fuel Stats")]
        [SerializeField] float maxFuel = 100f;
        [SerializeField] float fuelAmount = 100f;
        [SerializeField] float lowFuelAmount = 10f;
        [SerializeField] float fuelSpendAmount = 1f;

        bool hasFuel = true;

        ShipMovement shipMovement;

        void Start()
        {
            shipMovement = GetComponent<ShipMovement>();
        }

        void Update() 
        {
            CheckHasFuel();
            if (shipMovement.IsThrusting() && hasFuel)
            {
                SpendFuel();
            }
            if(fuelAmount > maxFuel)
            {
                fuelAmount = maxFuel;
            }
        }

        void SpendFuel()
        {
            fuelAmount -= fuelSpendAmount * Time.deltaTime;
        }

        public float GetFraction()
        {
            return fuelAmount / maxFuel;
        }

        void CheckHasFuel()
        {
            if (fuelAmount > 0)
            {
                hasFuel = true;
            }
            if (fuelAmount <= lowFuelAmount)
            {
                lowFuel.Invoke();
            }
            if (fuelAmount > lowFuelAmount)
            {
                fuelAboveLow.Invoke();
            }
            if (fuelAmount <= 0)
            {
                hasFuel = false;
                outOfFuel.Invoke();
            }
        }

        public void RestoreFuel(float fuelToRestore)
        {
            if(fuelAmount >= maxFuel)
            {
                return;
            }
            else
            {
                fuelAmount += fuelToRestore;
            }
        }
        

    }
}
