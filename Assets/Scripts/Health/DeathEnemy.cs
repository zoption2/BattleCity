using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEnemy : Death
{
    public override void DeathRattle()
    {
        if (isDead == false)
        {
            isDead = true;
            EnemyAI enemy = GetComponent<EnemyAI>();
            enemy.enemyLife = EnemyLife.dead;
            //TotalSpawner.spawn.SpawnFromSpawner("Explosion", transform.position, transform.rotation);
            VFXTotalSpawner.Instance.PlayEffect("Explosion_1", transform.position, Quaternion.identity, 0.8f);
            animator.SetBool("isDeadAnimation", true);
            soundManager.PlayShortAudio(soundManager.explosionSound(), 0.4f, true);
            ShowPoints();
        }
    }

    public override void Die()
    {
        whoIsKiller = health.GetKillerName();
        if (whoIsKiller == null) SelfDeath();

        switch (whoIsDead)
        {
            case "SmallEnemy":
                MasterController.smallTanks[whoIsKiller]++;
               // BonusPoints.ShowPoints(100, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z));
                levelManager.DestroyEnemy(enemy);
                break;
            case "FastEnemy":
                MasterController.fastTanks[whoIsKiller]++;
               // BonusPoints.ShowPoints(200, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z));
                levelManager.DestroyEnemy(enemy);
                break;
            case "BigEnemy":
                MasterController.bigTanks[whoIsKiller]++;
               // BonusPoints.ShowPoints(300, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z));
                levelManager.DestroyEnemy(enemy);
                break;
            case "ArmoredTank":
                MasterController.armoredTanks[whoIsKiller]++;
               // BonusPoints.ShowPoints(400, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z));
                levelManager.DestroyEnemy(enemy);
                break;
            default: 
                break;
        }

        playersStats.SetProjectiles(whoIsKiller, 1);
        Destroy(enemy);
    }

    private void ShowPoints()
    {
        string name = gameObject.tag;
        switch (name)
        {
            case "SmallEnemy":
                BonusPoints.ShowPoints(100, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z));
                break;
            case "FastEnemy":
                BonusPoints.ShowPoints(200, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z));
                break;
            case "BigEnemy":
                BonusPoints.ShowPoints(300, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z));
                break;
            case "ArmoredTank":
                BonusPoints.ShowPoints(400, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z));
                break;
            default:
                break;
        }
    }
}
