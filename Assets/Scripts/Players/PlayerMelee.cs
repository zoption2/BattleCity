using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour, I_PlayerSpecific
{
    private Player player;
    private PlayersStats playersStats;
    private GameObject spikes;
    private EnergyShield energyShield;
    private string _thisPlayer;

    private void Start()
    {
        player = GetComponent<Player>();
        _thisPlayer = this.gameObject.tag;
        player.OverrideOriginalSpeed(6);
        playersStats = PlayersStats.Instance;
        var conteinerSpikes = PrefabsConteiner.instance.spikes;
        spikes = Instantiate(conteinerSpikes, transform);
        var spike = spikes.GetComponent<Spikes>();
        spike.player = player;
        var weapon = spikes.GetComponent<Missile>();
        weapon.SetShooter(this.gameObject);
        energyShield = GetComponent<EnergyShield>();
    }

    public void FirstSkill(string playerName)
    {
        if (MasterController.playerBoosters[playerName] > 0)
        {
            var starboost = playersStats.GetStars(playerName) * 0.3f;

            StartCoroutine(speedModifier(1f + starboost, 2f));
            playersStats.SetProjectiles(playerName, -1);
        }
    }

    public void SecondSkill(string playerName)
    {
        if (MasterController.playerBoosters[playerName] > 1)
        {
            var spawnSpeed = TotalSpawner.spawn.SpawnFromSpawner("SpeedBuff", transform.position + transform.TransformDirection(new Vector3(0, 0, 3)), Quaternion.LookRotation(-Vector3.forward));
            playersStats.SetProjectiles(playerName, -2);
        }
        
    }

    public void ThirdSkill(string playerName)
    {
        if (MasterController.playerBoosters[playerName] > 2)
        {
            Assistent.assist.AssistTimerBuff();
            playersStats.SetProjectiles(playerName, -3);
        }
    }

    private IEnumerator speedModifier(float time, float modifier)
    {
        player.SetSpeed(true, modifier);
        energyShield.CustomImmortality(time);
        yield return new WaitForSeconds(time);
        player.SetSpeed(false);
    }

    protected void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<Buffs>() != null)
        {
            collider.gameObject.GetComponent<Buffs>().BuffMe(gameObject);
        }
    }
}
