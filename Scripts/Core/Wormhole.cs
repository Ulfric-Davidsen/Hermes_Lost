using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IHS.Managers;

public class Wormhole : MonoBehaviour
{
    [Header("Wormhole Object")]
    [SerializeField] GameObject wormhole;

    [Header("Scan FX")]
    [SerializeField] ParticleSystem scanFX = null;

    [Header("Ranges")]
    [SerializeField] float activationRange = 50f;
    [SerializeField] float scanRange = 300f;

    [Header("Level Conditions")]
    [SerializeField] bool levelConditions = true;

    public LayerMask whatIsPlayer;

    bool playerInActivationRange;
    bool playerInScanRange;
    bool ableToActivate = false;


    void Start()
    {
        GameManager.LevelConditionsMet += AbleToActivateWormhole;
        GameManager.ScanForWormhole += CheckPlayerScanDistance;
        wormhole.SetActive(false);

        CheckLevelConditions();
    }

    
    void Update()
    {
        playerInActivationRange = Physics.CheckSphere(transform.position, activationRange, whatIsPlayer);

        if(ableToActivate && playerInActivationRange)
        {
            ActivateWormhole();
        }
    }

    void CheckLevelConditions()
    {
        if(levelConditions == false)
        {
            GameManager.LevelConditionsMetEvent();
        }
    }

    void CheckPlayerScanDistance()
    {
        playerInScanRange = Physics.CheckSphere(transform.position, scanRange, whatIsPlayer);

        if(!playerInScanRange)
        {
            Debug.Log("NO ANOMOLIES DETECTED");
        }
        if(playerInScanRange)
        {
            scanFX.Play();
        }
        
    }

    void AbleToActivateWormhole()
    {
        ableToActivate = true;
    }

    void ActivateWormhole()
    {
        wormhole.SetActive(true);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, activationRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, scanRange);
    }

    void OnDisable()
    {
        GameManager.LevelConditionsMet -= AbleToActivateWormhole;
        GameManager.ScanForWormhole -= CheckPlayerScanDistance;
    }

}
