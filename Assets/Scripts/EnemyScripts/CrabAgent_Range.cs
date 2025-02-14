using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static PlayerSkillsystem;


public class CrabAgent_Range : MonoBehaviour
{
    public static CrabAgent_Range rangedCrab;
    private OverallEnemy enemy;
    private Transform movePositionTransform;
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private FoVScript fov;
    private Vector3 spawnpoint;
    private GameObject projectileSpawnpoint;
    private float shotSpeed;
    private float fireRate;
    private float fireBallDamage;
    private EnemyHealthHandler health;

    [SerializeField] 
    GameObject fireball;

    public float FireBallDamage { get => fireBallDamage; set => fireBallDamage = value; }

    /// <summary>
    /// References set to all necessary Context
    /// </summary>
    private void Awake()
    {
        rangedCrab = this;
        enemy = GetComponent<OverallEnemy>();
        fov = GetComponent<FoVScript>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        movePositionTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        projectileSpawnpoint = GetComponentInChildren<Spawnpoint>().gameObject;
        animator = GetComponent<Animator>();
        health = GetComponentInChildren<EnemyHealthHandler>();

        spawnpoint = this.transform.position;

        fireRate = 5.0f;
        shotSpeed = 20.0f;

        fireBallDamage = 5 + enemy.Playerlevel * 2 ;
        health.Health = 250 + enemy.Playerlevel * 5;
    }

    /// <summary>
    /// checking for Target
    /// checking for incoming Damage
    /// </summary>
    private void Update()
    {
        enemy.GetDamage("Take Damage", "Die", 250);
        Attack();
        fireRate -= Time.deltaTime;

        lookAt();
    }

    /// <summary>
    /// if the Enemy can see the Player the Enemy is always looking in the direction of the Player
    /// </summary>
    private void lookAt()
    {
        if (fov.CanSeePlayer)
        {
            Vector3 relativePos = movePositionTransform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            this.transform.rotation = rotation;
        }
    }

    /// <summary>
    /// if the Enemy can see the Player the Enemy is jumping and every 5 seconds shooting a fireball
    /// is the Enemy cant see the Player the Enemy is no more jumping
    /// </summary>
    private void Attack()
    {
        if (fov.CanSeePlayer)
        {
            animator.SetBool("Jump", true);
            navMeshAgent.destination = movePositionTransform.position;
            if(fireRate <= 0)
            {
                animator.SetTrigger("Cast Spell");
                fireRate = 5.0f;
            }
        }
        if (!fov.CanSeePlayer)
        {
            navMeshAgent.destination = spawnpoint;
            animator.SetBool("Jump", false);
        }
    }

    /// <summary>
    /// a fireball is spawning in the on the spawnpoint and with directionall force towards the Player
    /// </summary>
    private void SpawnBullet()
    {
            if (movePositionTransform != null)
            {
                GameObject fireBall = Instantiate(fireball, projectileSpawnpoint.transform.position, Quaternion.identity);
                Vector3 direction = movePositionTransform.position - (projectileSpawnpoint.transform.position - new Vector3(0, 1, 0));

                fireBall.GetComponent<Rigidbody>().AddForce(direction.normalized * shotSpeed, ForceMode.Impulse);
            }
    }
}
