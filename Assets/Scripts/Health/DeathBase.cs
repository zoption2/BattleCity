using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBase : Death
{
    public override void DeathRattle()
    {
        Die();
    }

    public override void Die()
    {
        TotalSpawner.spawn.SpawnFromSpawner("Explosion", transform.position, transform.rotation);
        GameplayManager GPM = GameObject.Find("Canvas").GetComponent<GameplayManager>();
        GPM.GameOver();
    }
}
