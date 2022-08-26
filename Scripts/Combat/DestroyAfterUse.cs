using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterUse : MonoBehaviour
{
    void Start()
    {
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();

        float life = particleSystem.main.duration;
        Destroy(gameObject, life);
    }

}
