using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefender : MonoBehaviour, I_PlayerSpecific
{
    private static int layerEnemy, layerRedWall, layerBorder, layerWater, layerPlayer;
    private static int maskEnemy, maskRedWall, maskBorder, maskWater, maskPlayer;
    private int layerMask;
    private string _thisPlayer;
    private GameObject turret;

    private void Start()
    {
        _thisPlayer = this.gameObject.tag;

       layerEnemy = LayerMask.NameToLayer("Enemy");
       layerRedWall = LayerMask.NameToLayer("RedWall");
       layerBorder = LayerMask.NameToLayer("Border");
       layerWater = LayerMask.NameToLayer("Water");
       layerPlayer = LayerMask.NameToLayer("Player");

       maskEnemy = 1 << layerEnemy;
       maskRedWall = 1 << layerRedWall;
       maskBorder = 1 << layerBorder;
       maskWater = 1 << layerWater;
       maskPlayer = 1 << layerPlayer;

       layerMask = maskEnemy | maskRedWall | maskBorder | maskWater | maskPlayer;
    }

    public void FirstSkill(string playerName)
    {
        if (MasterController.playerBoosters[playerName] > 0)
        {
            GameObject mine;
            mine = (GameObject)TotalSpawner.spawn.SpawnFromSpawner("Mine", this.transform.position, Quaternion.LookRotation(transform.forward));
            // MasterController.whoIsShooter[mine.GetInstanceID()] = this.gameObject.tag;
            var _missile = mine.GetComponent<Missile>();
            _missile.SetShooter(this.gameObject);
            //MasterController.playerBoosters[playerName] -= 1;
            PlayersStats.Instance.SetProjectiles(playerName, -1);
        }
    }

    public void SecondSkill(string playerName)
    {
        if (MasterController.playerBoosters[playerName] > 1)
        {
            var Unity = MasterController.activePlayers.Values;
            foreach (var item in Unity)
            {
                item.GetComponent<EnergyShield>().PlayerDefenderBuff();
            }
            PlayersStats.Instance.SetProjectiles(playerName, -2);
        }
    }

    public void ThirdSkill(string playerName)
    {
        if (MasterController.playerBoosters[playerName] > 2)
        {
            var isBlocking = Physics.Linecast(transform.position + new Vector3(0,1,0), transform.position + transform.TransformDirection(new Vector3(0,1,3)), layerMask);
            if (!isBlocking)
            {
                var prefab = PrefabsConteiner.instance.turret;
                if (turret != null)
                {
                    turret.GetComponent<TurretPlayer>().DestroyTurret();
                }
                turret = (GameObject)Instantiate(prefab, transform.position + transform.TransformDirection(new Vector3(0, 0, 2)), Quaternion.LookRotation(transform.forward));
                turret.GetComponent<TurretPlayer>().player = this.gameObject;
                turret.GetComponent<TurretPlayer>().GetStarted();

                //MasterController.playerBoosters[playerName] -= 3;
                PlayersStats.Instance.SetProjectiles(playerName, -3);
            }
            
        }
    }

    protected void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<Buffs>() != null)
        {
            collider.gameObject.GetComponent<Buffs>().BuffMe(gameObject);
        }
    }

    private void OnDisable()
    {
        turret.GetComponent<TurretPlayer>().DestroyTurret();
    }
}
