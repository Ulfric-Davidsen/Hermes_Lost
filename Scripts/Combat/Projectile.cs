using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IHS.Core;

namespace IHS.Combat
{
    public class Projectile : MonoBehaviour
    {
        [Header("Projectile Stats")]
        [SerializeField] float speed = 100f;
        [SerializeField] float maxLifeTime = 7f;
        [SerializeField] float lifeAfterImpact = 1f;
        [SerializeField] float damage = 10f;

        [Header ("Hit VFX")]
        [SerializeField] GameObject hitVFX = null;

        void Update()
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            Destroy(gameObject, maxLifeTime);
        }

        void OnCollisionEnter(Collision collision)
        {   
            if (collision.gameObject.TryGetComponent<Health>(out Health healthComponent))
            {
                healthComponent.TakeDamage(damage);
            }
            speed = 0;
            if(hitVFX != null)
            {
                Instantiate(hitVFX, transform.position, transform.rotation);
            }
            Destroy(gameObject, lifeAfterImpact);
        }

    }

}
