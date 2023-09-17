using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HL.Items
{
    public class RandomItemSpawner : MonoBehaviour
    {
        ///un-comment code once raw materials are added to game///

        [Header ("Items To Spawn")]
        [SerializeField] GameObject highProbabilityItem;
        // [SerializeField] GameObject midProbabilityItem;
        [SerializeField] GameObject lowProbabilityItem;

        [Header ("Probability Ranges")]
        [SerializeField] int lowerRange = 15;
        // [SerializeField] int upperRange = 45;

        [Header ("Item Spawn Transform")]
        [SerializeField] Transform itemSpawn;

        int value;
        
        public void DropItem()
        {
            value = Random.Range(0, 101);

            if(value <= lowerRange)
            {
                Instantiate(lowProbabilityItem, itemSpawn.position, itemSpawn.rotation);
            }
            else
            {
                Instantiate(highProbabilityItem, itemSpawn.position, itemSpawn.rotation);
            }

            // if(value <= lowerRange)
            // {
            //     Instantiate(lowProbabilityItem, itemSpawn.position, itemSpawn.rotation);
            // }
            // else if(value <= upperRange && > lowerRange)
            // {
            //     Instantiate(midProbabilityItem, itemSpawn.position, itemSpawn.rotation);
            // }
            // else
            // {
            //     Instantiate(highProbabilityItem, itemSpawn.position, itemSpawn.rotation);
            // }
        }
    }
}
