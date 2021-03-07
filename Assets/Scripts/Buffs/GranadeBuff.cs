using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeBuff : Buffs, I_Buffs
{
    private List<GameObject> enemyAI;

    public override void BuffMechanic(GameObject player)
    {
        string playerName = player.tag;
        enemyAI = LevelManager.Instance.GetAllEnemies();

        foreach (var enemy in enemyAI)
        {
            var rocket = TotalSpawner.spawn.SpawnFromSpawner("MassStrike", new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.LookRotation(transform.up));
            var launcher = rocket.GetComponent<MassStrike>();
            launcher.enemy = enemy;
            launcher._launcherPos = transform.position;
        }

        this.gameObject.SetActive(false);
    }

    public override void BuffMechanic(GameObject player_1, GameObject player_2)
    {
        string playerName = player_1.tag;
        string secondPlayer = player_2.tag;
        enemyAI = LevelManager.Instance.GetAllEnemies();

        foreach (var enemy in enemyAI)
        {
            var rocket = TotalSpawner.spawn.SpawnFromSpawner("MassStrike", new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.LookRotation(transform.up));
            var launcher = rocket.GetComponent<MassStrike>();
            launcher.enemy = enemy;
            launcher._launcherPos = transform.position;
        }

        this.gameObject.SetActive(false);
    }
}
