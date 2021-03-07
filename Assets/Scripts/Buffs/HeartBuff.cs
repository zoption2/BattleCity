using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBuff : Buffs, I_Buffs
{
    public override void BuffMechanic(GameObject player)
    {
        string playerName = player.tag;
        PlayersStats.Instance.SetLives(playerName, 1);
        VFXTotalSpawner.Instance.PlayEffect("LivesUp", player.transform.position, Quaternion.identity, 2f, player.transform);

        this.gameObject.SetActive(false);
    }

    public override void BuffMechanic(GameObject player_1, GameObject player_2)
    {
        string playerName = player_1.tag;
        string secondPlayer = player_2.tag;
        PlayersStats.Instance.SetLives(playerName, 1);
        PlayersStats.Instance.SetLives(secondPlayer, 1);
        VFXTotalSpawner.Instance.PlayEffect("LivesUp", player_1.transform.position, Quaternion.identity, 2f, player_1.transform);
        VFXTotalSpawner.Instance.PlayEffect("LivesUp", player_2.transform.position, Quaternion.identity, 2f, player_2.transform);

        this.gameObject.SetActive(false);
    }
}
