using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabsConteiner : MonoBehaviour
{
    public static PrefabsConteiner instance;


    public GameObject player_1;
    public GameObject player_2;
    public GameObject player_3;
    public GameObject player_4;

    public GameObject smallEnemy;
    public GameObject fastEnemy;
    public GameObject bigEnemy;
    public GameObject armoredEnemy;
    public GameObject bossEnemy;

    public GameObject energyShield;
    public GameObject turret;
    public GameObject redWallBlock;
    public GameObject spikes;
    public GameObject riffleGun;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
