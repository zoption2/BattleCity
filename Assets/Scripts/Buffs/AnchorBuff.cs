using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorBuff : Buffs, I_Buffs
{
    public override void BuffMechanic(GameObject player)
    {
        string playerName = player.tag;
        MasterController.buffAnchorActive[playerName] = true;

        this.gameObject.SetActive(false);
    }


    public override void BuffMechanic(GameObject player_1, GameObject player_2)
    {
        string playerName = player_1.tag;
        string secondPlayer = player_2.tag;
        MasterController.buffAnchorActive[playerName] = true;
        MasterController.buffAnchorActive[secondPlayer] = true;

        this.gameObject.SetActive(false);
    }
}
