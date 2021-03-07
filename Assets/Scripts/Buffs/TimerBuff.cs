using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBuff : Buffs, I_Buffs
{
    public override void BuffMechanic(GameObject player)
    {
        Assistent.assist.AssistTimerBuff();
        this.gameObject.SetActive(false);
    }

    public override void BuffMechanic(GameObject player_1, GameObject player_2)
    {
        Assistent.assist.AssistTimerBuff();
        this.gameObject.SetActive(false);
    }
}
