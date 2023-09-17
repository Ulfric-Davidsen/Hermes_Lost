using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HL.Managers;
using HL.Combat;

namespace HL.Core
{
    public class Player : MonoBehaviour
    {
        [Header("Player Movement")]
        [SerializeField] float movementSpeed = 200f;
        [SerializeField] float rotationSpeed = 75f;

        [Header("Attacking")]
        [SerializeField] Projectile projectile = null;
        [SerializeField] Transform projectileSpawnPoint = null;
        [SerializeField] float timeBetweenAttacks = 1f;

        [Header("Scanning")]
        [SerializeField] ParticleSystem scanParticles = null;
        [SerializeField] Transform scanSpawnPoint = null;
        [SerializeField] float timeBetweenScans = 1f;

        float playerRotation = 0f;
        bool isFiring = false;
        bool isMoving = false;
        bool alreadyAttacked;
        bool alreadyScanned;

        Rigidbody rb;
        Energy energy;
        Animator animator;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            energy = GetComponent<Energy>();
            animator = GetComponentInChildren<Animator>();
        }

        void Update()
        {
            ProcessMovement();
            ProcessFiring();
            ProcessScanning();
        }

        void ProcessMovement()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 movementDirection = new Vector3(0 , 0, verticalInput);
            movementDirection.Normalize();

            if(verticalInput < 0 || verticalInput > 0)
            {
                rb.AddForce(transform.forward * verticalInput * movementSpeed * Time.deltaTime);
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }

            playerRotation += horizontalInput * rotationSpeed * Time.deltaTime;
            transform.eulerAngles = new Vector3 (0f, playerRotation, 0f);

            animator.SetFloat("VelocityX", horizontalInput);
            animator.SetFloat("VelocityZ", verticalInput);
        }

        void ProcessFiring()
        {
            if(Input.GetMouseButtonDown(0))
            {   
                if (!energy.HasEnergy())
                {
                    isFiring = false;
                    return;
                }
                if (energy.HasEnergy() && !alreadyAttacked)
                {
                    LaunchProjectile();
                    isFiring = true;
                    alreadyAttacked = true;
                    Invoke(nameof(ResetAttack), timeBetweenAttacks);
                } 
            }
            else
            {
                isFiring = false;
            }

        }

        void ResetAttack()
        {
            alreadyAttacked = false;
        }

        void LaunchProjectile()
        {
            Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        }

        void ProcessScanning()
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                if(!alreadyScanned)
                {
                    Instantiate(scanParticles, scanSpawnPoint.position, scanSpawnPoint.rotation);
                    GameManager.ScanForWormholeEvent();
                    alreadyScanned = true;
                    Invoke(nameof(ResetScan), timeBetweenScans);
                } 
                
            }
        }

        void ResetScan()
        {
            alreadyScanned = false;
        }

        public bool IsFiring()
        {
            return isFiring;
        }

        public bool IsMoving()
        {
            return isMoving;
        }

    }
}
