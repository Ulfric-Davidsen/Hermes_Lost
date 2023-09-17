using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using HL.Core;
using HL.Combat;

namespace HL.AI
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
            player = GameObject.Find("Player").transform;
            agent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
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

            if (distanceToWalkPoint.magnitude < 1f)
            {
                walkPointSet = false;
            }
        }

        void SearchWalkPoint()
        {
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
            agent.SetDestination(transform.position);

            transform.LookAt(player);

            if (!alreadyAttacked)
            {
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
