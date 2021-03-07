using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeBuff : Buffs, I_Buffs
{
    public override void BuffMechanic(GameObject player)
    {
        string playerName = player.tag;
        MasterController.buffAxeActive[playerName] = true;

        this.gameObject.SetActive(false);
    }

    public override void BuffMechanic(GameObject player_1, GameObject player_2)
    {
        string playerName = player_1.tag;
        string secondPlayer = player_2.tag;
        MasterController.buffAxeActive[playerName] = true;
        MasterController.buffAxeActive[secondPlayer] = true;

        this.gameObject.SetActive(false);
    }
}
