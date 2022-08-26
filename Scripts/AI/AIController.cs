using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using IHS.Core;
using IHS.Combat;

namespace IHS.AI
{
    public class AIController : MonoBehaviour
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
        bool alreadyAttacked;

        [Header("Ranges")]
        [SerializeField] float sightRange;
        [SerializeField] float attackRange;

        bool playerInSightRange;
        bool playerInAttackRange;

        [Header("Projectile")]
        [SerializeField] Projectile projectile = null;
        [SerializeField] Transform projectileSpawnPoint = null;

        void Awake()
        {
            player = GameObject.Find("PlayerPrefab").transform;
            agent = GetComponent<NavMeshAgent>();
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

                // Debug.Log("FIRE PROJECTILE");
                LaunchProjectile();

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
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

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, sightRange);
        }

    }
}
