using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlayer : Death
{
    public override void DeathRattle()
    {
        Player player = GetComponent<Player>();
        player.alive = false;
        VFXTotalSpawner.Instance.PlayEffect("Explosion_1", transform.position, Quaternion.identity, 0.8f);
        //TotalSpawner.spawn.SpawnFromSpawner("Explosion", transform.position, transform.rotation);
        animator.SetBool("isDeadAnimation", true);
        soundManager.PlayShortAudio(soundManager.explosionSound(), 0.4f, true);
    }

    public override void Die()
    {
        animator.SetBool("isDeadAnimation", false);
        MasterController.isPlayerAlive[whoIsDead] = false;
        MasterController.activePlayers.Remove(whoIsDead);
        GPM.SpawnPlayer(whoIsDead);
        Destroy(this.gameObject);
    }
}
