using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HL.Core;

namespace HL.Items
{
    public class HealthPickup : MonoBehaviour
    {
        [SerializeField] float healthToRestore = 25f;
        [SerializeField] GameObject pickupVFX = null;
        [SerializeField] Transform vfxSpawn;

        void OnTriggerEnter(Collider collision)
        {
            if(collision.gameObject.tag == "Player")
            {
                collision.GetComponent<Health>().RestoreHealth(healthToRestore);
                
                SpawnVFX();
                DestroyPickup();
            }
        }

        void SpawnVFX()
        {
            Instantiate(pickupVFX, vfxSpawn.position, vfxSpawn.rotation);
        }

        void DestroyPickup()
        {
            Destroy(gameObject);
        }
    }
}
