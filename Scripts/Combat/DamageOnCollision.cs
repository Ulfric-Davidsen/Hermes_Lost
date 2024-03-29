using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IHS.Core;

namespace IHS.Combat
{
    public class DamageOnCollision : MonoBehaviour
    {
        [Header("Damage Given On Collision")]
        [SerializeField] float damage = 100f;

        void OnCollisionEnter(Collision collision)
        {   
            if (collision.gameObject.TryGetComponent<Health>(out Health healthComponent))
            {
                healthComponent.TakeDamage(damage);
            }
        }
    }
}
