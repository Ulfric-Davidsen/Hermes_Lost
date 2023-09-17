using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace HL.Core
{
    public class Health : MonoBehaviour
    {
        [Header("Health Stats")]
        [SerializeField] float maxHitPoints = 100f;
        [SerializeField] float hitPoints;

        [Header("Death Event")]
        [SerializeField] UnityEvent onDie;

        bool isDead = false;

        void Start()
        {
            hitPoints = maxHitPoints;
        }

        void Update()
        {
            if(hitPoints > maxHitPoints)
            {
                hitPoints = maxHitPoints;
            }
        }

        public void TakeDamage(float damage)
        {
            hitPoints -= damage;

            if(hitPoints <= 0)
            {
                onDie.Invoke();
                Die();
            }
        }

        public void RestoreHealth(float healthToRestore)
        {
            if(hitPoints >= maxHitPoints)
            {
                return;
            }
            else
            {
                hitPoints += healthToRestore;
            }
        }

        public float GetFraction()
        {
            return hitPoints / maxHitPoints;
        }

        public float GetMaxHitPoints()
        {
            return maxHitPoints;
        }

        public float GetHitPoints()
        {
            return hitPoints;
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void Die()
        {
            if(isDead) return;

            isDead = true;
        }
    }
}
