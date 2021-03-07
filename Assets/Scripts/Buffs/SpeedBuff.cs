using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBuff : Buffs, I_Buffs
{
    public override void BuffMechanic(GameObject player)
    {
        string playerName = player.tag;
        GameObject playerGO = MasterController.activePlayers[playerName]; 
        var play = playerGO.GetComponent<Player>();
        play.SpeedBoost = 1;

        this.gameObject.SetActive(false);
    }

    public override void BuffMechanic(GameObject player_1, GameObject player_2)
    {
        string playerName = player_1.tag;
        string secondPlayer = player_2.tag;
        //GameObject playerGO = MasterController.activePlayers[playerName]; 
        var player = player_1.GetComponent<Player>();
        player.SpeedBoost = 1;

       // GameObject playerGO2 = MasterController.activePlayers[secondPlayer];
        var player2 = player_2.GetComponent<Player>();
        player2.SpeedBoost = 1;

        this.gameObject.SetActive(false);
    }
}
