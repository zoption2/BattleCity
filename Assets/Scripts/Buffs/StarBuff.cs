using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBuff : Buffs, I_Buffs
{
    public override void BuffMechanic(GameObject player)
    {
        string playerName = player.tag;
        PlayersStats.Instance.SetStars(playerName, 1);

        this.gameObject.SetActive(false);
    }

    public override void BuffMechanic(GameObject player_1, GameObject player_2)
    {
        string playerName = player_1.tag;
        string secondPlayer = player_2.tag;
        PlayersStats.Instance.SetStars(playerName, 1);
        PlayersStats.Instance.SetStars(secondPlayer, 1);

        this.gameObject.SetActive(false);
    }
}
