using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using IHS.Core;
using IHS.Combat;

namespace IHS.AI
{
    public class AIBossController : MonoBehaviour
    {
        [SerializeField] NavMeshAgent agent;

        [SerializeField] Transform player;

        public LayerMask whatIsGround, whatIsPlayer;

        [Header("Patrolling")]
        [SerializeField] Vector3 walkPoint;
        [SerializeField] float walkPointRange;
        bool walkPointSet;

        [Header("Attacking")]
        [SerializeField] float timeBetweenAttacks;
        [SerializeField] float timeBetweenAttacksP1;
        [SerializeField] float timeBetweenAttacksP2;
        [SerializeField] float timeBetweenAttacksP3;
        bool alreadyAttacked;

        [Header("Ranges")]
        [SerializeField] float sightRange;
        [SerializeField] float attackRange;

        bool playerInSightRange;
        bool playerInAttackRange;

        [Header("Projectiles")]
        [SerializeField] Projectile projectilePhaseOne = null;
        [SerializeField] Projectile projectilePhaseTwo = null;
        [SerializeField] Projectile projectilePhaseThree = null;
        [SerializeField] Transform projectileSpawnPoint = null;

        [Header("Phase Booleans")]
        [SerializeField] bool inPhaseOne;
        [SerializeField] bool inPhaseTwo;
        [SerializeField] bool inPhaseThree;

        Health health;

        private void Awake()
        {
            player = GameObject.Find("PlayerPrefab").transform;
            agent = GetComponent<NavMeshAgent>();
        }

        void Start()
        {
            health = GetComponent<Health>();
        }

        void Update()
        {
            //Check for sight and attack range
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange)
            {
                Patrolling();
            }

            if (playerInSightRange && !playerInAttackRange)
            {
                ChasePlayer();
            }

            if (playerInAttackRange && playerInSightRange)
            {
                GetPhase();
                AttackPlayer();
            }
        }

        /// PRIVATE ///

        void Patrolling()
        {
            if (!walkPointSet)
            {
                SearchWalkPoint();
            }

            if (walkPointSet)
            {
                agent.SetDestination(walkPoint);
            }

            Vector3 distanceToWalkPoint = transform.position - walkPoint;

            //Walkpoint reached
            if (distanceToWalkPoint.magnitude < 1f)
            {
                walkPointSet = false;
            }
        }

        void SearchWalkPoint()
        {
            Debug.Log("PATROLLING");
            
            //Calculate random point in range
            float randomZ = Random.Range(-walkPointRange, walkPointRange);
            float randomX = Random.Range(-walkPointRange, walkPointRange);

            walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

            if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            {
                walkPointSet = true;
            }
        }

        void ChasePlayer()
        {
            Debug.Log("CHASE PLAYER");

            agent.SetDestination(player.position);
        }

        void AttackPlayer()
        {
            //Make sure enemy doesn't move
            agent.SetDestination(transform.position);

            transform.LookAt(player);

            if (!alreadyAttacked)
            {
                ///attack code here
                Debug.Log("FIRE PROJECTILE");
                CheckPhase();
                ///
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }

        void ResetAttack()
        {
            alreadyAttacked = false;
        }

        void CheckPhase()
        {
            if (inPhaseOne)
            {
                LaunchProjectileOne();
            }

            if (inPhaseTwo)
            {
                LaunchProjectileTwo();
            }

            if (inPhaseThree)
            {
                LaunchProjectileThree();
            }
        }

        void GetPhase()
        {
            if (health.GetHitPoints() >= health.GetMaxHitPoints() * 0.66f)
            {
                Debug.Log("In Phase ONE");
                inPhaseOne = true;
                inPhaseTwo = false;
                inPhaseThree = false;
                timeBetweenAttacks = timeBetweenAttacksP1;
            }
            if (health.GetHitPoints() <= health.GetMaxHitPoints() * 0.66f && health.GetHitPoints() >= health.GetMaxHitPoints() * 0.33f)
            {
                Debug.Log("In phase TWO");
                inPhaseOne = false;
                inPhaseTwo = true;
                inPhaseThree = false;
                timeBetweenAttacks = timeBetweenAttacksP2;
            }
            if (health.GetHitPoints() <= health.GetMaxHitPoints() * 0.33f)
            {
                Debug.Log("In phase THREE");
                inPhaseOne = false;
                inPhaseTwo = false;
                inPhaseThree = true;
                timeBetweenAttacks = timeBetweenAttacksP3;
            }
        }

        void LaunchProjectileOne()
        {
            Instantiate(projectilePhaseOne, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        }

        void LaunchProjectileTwo()
        {
            Instantiate(projectilePhaseTwo, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        }

        void LaunchProjectileThree()
        {
            Instantiate(projectilePhaseThree, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, sightRange);
        }
    }
}
