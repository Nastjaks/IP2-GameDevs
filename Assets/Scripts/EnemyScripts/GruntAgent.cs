using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CombatSystem;
using static EnemySoundHandler;

public class GruntAgent : MonoBehaviour
{
    private OverallEnemy enemy;
    private PlayerAttributes player;
    private EnemyHealthHandler health;
    private bool doDamage;

    private float damage;

    /// <summary>
    /// References set to all necessary Context
    /// </summary>
    private void Awake()
    {
        enemy = GetComponent<OverallEnemy>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttributes>();
        health = GetComponentInChildren<EnemyHealthHandler>();

        health.Health = 400 + enemy.Playerlevel * 5;
        damage = 5 + enemy.Playerlevel * 3;
    }

    /// <summary>
    /// timer counting while Update
    /// checking for Target
    /// checking for incoming Damage
    /// </summary>
    private void Update()
    {
        enemy.WalkOrAttack("Run", "Attack1", "Attack2", 5, 15, 0);
        enemy.GetDamage("Take Damage", "Die", 350);
    }

    /// <summary>
    /// if the Enemy is able to hit the Player, the Player is getting damaged.
    /// </summary>
    private void DoDamage()
    {
        if (doDamage)
        {
            enemySoundhandler.hitSound();
            combatSystem.LoseHealth(damage);
            doDamage = false;
        }
    }

    /// <summary>
    /// if the triggerCollider is entered the Enemy is doing Damage
    /// </summary>
    /// <param name="other">the triggering Collider has to have the Tag Player</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            doDamage = true;
        }
    }
}
