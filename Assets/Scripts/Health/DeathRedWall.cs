using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathRedWall : Death
{
    public override void DeathRattle()
    {
        Die();
    }

    public override void Die()
    {
        var redWall = TotalSpawner.spawn.SpawnFromSpawner("RedWallDestroy", transform.position, Quaternion.identity);
        redWall.GetComponent<TailVFX>().LifeOfTail();

        this.gameObject.SetActive(false);
    }
}
