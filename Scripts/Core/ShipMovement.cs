using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using IHS.Managers;
using IHS.Combat;

namespace IHS.Core
{
    public class ShipMovement : MonoBehaviour
    {
        [Header("Ship Controls")]
        [SerializeField] float forwardThrust = 1000f;
        [SerializeField] float rotationSpeed = 100f;

        [Header("Ship Audio")]
        [SerializeField] AudioClip mainEngine = null;

        [Header("Ship Particles")]
        [SerializeField] ParticleSystem forwardThrustParticles = null;
        [SerializeField] ParticleSystem rightThrusterParticles = null;
        [SerializeField] ParticleSystem leftThrusterParticles = null;

        [Header("Attacking")]
        [SerializeField] Projectile projectile = null;
        [SerializeField] Transform projectileSpawnPoint = null;
        [SerializeField] float timeBetweenAttacks = 1f;

        [Header("Scanning")]
        [SerializeField] ParticleSystem scanParticles = null;
        [SerializeField] Transform scanSpawnPoint = null;
        [SerializeField] float timeBetweenScans = 1f;

        float rotationOfShip = 0f;
        bool isThrusting = false;
        bool isFiring = false;
        bool alreadyAttacked;
        bool alreadyScanned;
        bool isTransitioning = false;

        Rigidbody rb;
        AudioSource audioSource;
        Energy energy;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            audioSource = GetComponent<AudioSource>();
            energy = GetComponent<Energy>();
        }

        void Update()
        {
            if(isTransitioning) {return;}

            ProcessThrust();
            ProcessRotation();
            ProcessFiring();
            ProcessScanning();
        }

        //PRIVATE//
        void ProcessThrust()
        {
            if(Input.GetButton("Jump"))
            {
                StartThrusting();
                isThrusting = true;
            }
            else
            {
                StopThrusting();
                isThrusting = false;
            }
        }

        void StartThrusting()
        {
            rb.AddRelativeForce(Vector3.forward * forwardThrust * Time.deltaTime); //Adds upward force relative to the gameobject that the script is applied to

            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            if(!forwardThrustParticles.isPlaying)
            {
                forwardThrustParticles.Play();
            }
        }

        void ProcessRotation()
        {
            rotationOfShip -= Input.GetAxis ("Horizontal") * rotationSpeed * Time.deltaTime;
            transform.eulerAngles = new Vector3 (0f, rotationOfShip, 0f);

            //Reads Horizontal axis for range from -1 to 1. Activates method if Horizontal axis is any value greater than 0.
            if(Input.GetAxis("Horizontal") > 0f)
            {
                RotateLeft();
            }
            //Reads Horizontal axis for range from -1 to 1. Activates method if Horizontal axis is any value less than 0.
            else if(Input.GetAxis("Horizontal") < 0f)
            {
                RotateRight();
            }
        
            else
            {
                StopRotation();
            }
        }
        void RotateLeft()
        {
            if(!rightThrusterParticles.isPlaying) //If particle isn't playing, play particle.
            {
                rightThrusterParticles.Play();
            }
        }
        void RotateRight()
        {
            if(!leftThrusterParticles.isPlaying) //If particle isn't playing, play particle.
            {
                leftThrusterParticles.Play();
            }
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

        //PUBLIC//
        public void StopThrusting()
        {
            audioSource.Stop();
            forwardThrustParticles.Stop();
        }

        public void StopRotation()
        {
            rightThrusterParticles.Stop();
            leftThrusterParticles.Stop();
        }

        public bool IsThrusting()
        {
            return isThrusting;
        }

        public bool IsFiring()
        {
            return isFiring;
        }

    }
}
