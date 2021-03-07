using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_Buffs
{
    void BuffMe(GameObject player);
    void BuffMechanic(GameObject player);
    void BuffMechanic(GameObject player_1, GameObject player_2);

}

public abstract class Buffs : MonoBehaviour, I_Buffs
{
    private GameObject player_1;
    private GameObject player_2;

     public void BuffMe(GameObject player)
    {
        string playerName = player.tag;
        BuffMechanic(player);
        MasterController.bonusScores[playerName] += 100;
        BonusPoints.ShowPoints(100, new Vector3(transform.position.x, 3, transform.position.z));
        //var GO = TotalSpawner.spawn.SpawnFromSpawner("BonusText", new Vector3(transform.position.x, 3, transform.position.z), Quaternion.identity);
        //GO.GetComponent<TailVFX>().LifeOfTail();
    }
    public void BuffMe(GameObject player_1, GameObject player_2)
    {
        string playerName = player_1.tag;
        string secondName = player_2.tag;
        BuffMechanic(player_1, player_2);
        MasterController.bonusScores[playerName] += 200;
        BonusPoints.ShowPoints(200, new Vector3(transform.position.x, 3, transform.position.z));
        //var GO = TotalSpawner.spawn.SpawnFromSpawner("BonusText", new Vector3(transform.position.x, 3, transform.position.z), Quaternion.identity);
        //GO.GetComponent<TailVFX>().LifeOfTail();
    }
    public abstract void BuffMechanic(GameObject player);
    public abstract void BuffMechanic(GameObject player_1, GameObject player_2);

 
    public void OnDisable()
    {
        TotalSpawner.spawn.SpawnFromSpawner("TakeBuff", gameObject.transform.position, gameObject.transform.rotation);
        this.gameObject.SetActive(false);
    }

}

