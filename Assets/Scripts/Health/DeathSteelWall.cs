using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSteelWall : Death
{
    public override void DeathRattle()
    {
        Die();
    }

    public override void Die()
    {
        this.gameObject.SetActive(false);
    }
}
