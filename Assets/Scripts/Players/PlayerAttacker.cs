using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour, I_PlayerSpecific
{
    private Transform target;
    private SoundManager soundManager;
    private string _thisPlayer;
    private Coroutine _homileMissile;
    private void Start()
    {
        target = gameObject.transform.GetChild(1);
        _thisPlayer = this.gameObject.tag;
        soundManager = SoundManager.play;
    }

    public void FirstSkill(string playerName)
    {
        if (MasterController.playerBoosters[playerName] > 0)
        {
            GameObject missile;
            missile = (GameObject)TotalSpawner.spawn.SpawnFromSpawner("Laser", target.transform.position, Quaternion.LookRotation(transform.forward));
            var _missile = missile.GetComponent<Laser>();
            _missile.SetShooter(this.gameObject);
            _missile.DoRayShoot();

            //MasterController.playerBoosters[playerName]--;
            PlayersStats.Instance.SetProjectiles(playerName, -1);
        }
    }

    public void SecondSkill(string playerName)
    {
        if (MasterController.playerBoosters[playerName] > 1)
        {
            StartCoroutine(_launchHomileRocket(playerName));

            PlayersStats.Instance.SetProjectiles(playerName, -2);
        }
    }

    private IEnumerator _launchHomileRocket(string name)
    {
        LaunchHomileRocket();

        yield return new WaitForSeconds(0.2f);

        var starBonus = PlayersStats.Instance.GetStars(name);
        if (starBonus == 5)
        {
            LaunchHomileRocket();
        }
    }
    private void LaunchHomileRocket()
    {
        GameObject missile;
        missile = (GameObject)TotalSpawner.spawn.SpawnFromSpawner("HomileMissile", new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.LookRotation(transform.up));
        var _missile = missile.GetComponent<HomileMissile>();
        _missile.SetShooter(this.gameObject);
        _missile.enemy = LookOnTarget();
    }

    public void ThirdSkill(string playerName)
    {
        if (MasterController.playerBoosters[playerName] > 2)
        {
            StartCoroutine(BigLaserJob());
            // MasterController.playerBoosters[playerName] -=3;
            PlayersStats.Instance.SetProjectiles(playerName, -3);
        }
    }

    private GameObject LookOnTarget()
    {
        var targets = LevelManager.Instance.GetAllEnemies();
        var myTarget = targets[Random.Range(0, targets.Count)];
        return myTarget;
    }

    private IEnumerator BigLaserJob()
    {
        GameObject missile;
        GameObject laserCharge;
        laserCharge = TotalSpawner.spawn.SpawnFromSpawner("BigLaserCharge", new Vector3(target.transform.position.x, target.transform.position.y + 0.1f, target.transform.position.z), Quaternion.LookRotation(transform.forward));
        laserCharge.GetComponent<TailVFX>().LifeOfTail();
        soundManager.PlayShortAudio("BigLaserCharge", 0.5f);

        while (laserCharge.activeSelf)
        {
            laserCharge.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 0.1f, target.transform.position.z);
            yield return new WaitForFixedUpdate();
        }

        //yield return new WaitForSeconds(1.5f);

        missile = (GameObject)TotalSpawner.spawn.SpawnFromSpawner("BigLaser", target.transform.position, Quaternion.LookRotation(transform.forward));
        var _missile = missile.GetComponent<BigLaser>();
        _missile.SetShooter(this.gameObject);
        _missile.DoRayShoot();
        soundManager.PlayShortAudio("BigLaser", 0.4f);
    }

    protected void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<Buffs>() != null)
        {
            collider.gameObject.GetComponent<Buffs>().BuffMe(gameObject);
        }
    }
}
