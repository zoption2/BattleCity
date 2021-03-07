using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTurret : Death
{
    public override void DeathRattle()
    {
        Die();
    }

    public override void Die()
    {
        soundManager.PlayShortAudio(soundManager.explosionSound(), 0.4f, true);
        Destroy(this.gameObject);
    }
}
