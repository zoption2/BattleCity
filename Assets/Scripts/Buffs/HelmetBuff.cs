using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetBuff : Buffs, I_Buffs
{
    private GameObject player;

    public override void BuffMechanic(GameObject player)
    {
        string playerName = player.tag;
        player = MasterController.activePlayers[playerName];
        player.GetComponent<EnergyShield>().BuffImmortality();
        this.gameObject.SetActive(false);
    }

    public override void BuffMechanic(GameObject player_1, GameObject player_2)
    {
        string playerName = player_1.tag;
        string secondPlayer = player_2.tag;
        player = MasterController.activePlayers[playerName];
        player.GetComponent<EnergyShield>().BuffImmortality();

        player = MasterController.activePlayers[secondPlayer];
        player.GetComponent<EnergyShield>().BuffImmortality();

        this.gameObject.SetActive(false);
    }
}
