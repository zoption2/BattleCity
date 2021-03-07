using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Health))]
public abstract class Death : MonoBehaviour
{
    protected string whoIsKiller;
    protected string whoIsDead;
    protected bool isDead;
    protected GameObject enemy;
    protected Animator animator;
    protected Health health;
    protected SoundManager soundManager;
    protected LevelManager levelManager;
    protected GameplayManager GPM;
    protected PlayersStats playersStats;
    private void Start()
    {
        enemy = this.gameObject;
        whoIsDead = enemy.tag;
        health = GetComponent<Health>();
        soundManager = SoundManager.play;
        levelManager = LevelManager.Instance;
        playersStats = PlayersStats.Instance;
        GPM = GameObject.Find("Canvas").GetComponent<GameplayManager>();
        isDead = false;

        if (GetComponent<Animator>() != null)
        {
            animator = GetComponent<Animator>();
        }

        health.OnDeath += DeathRattle;
    }


    protected void SelfDeath()
    {
        isDead = true;
        levelManager.DestroyEnemy(enemy);
        Destroy(enemy);
    }

    public abstract void DeathRattle();
    public abstract void Die();
}
    





