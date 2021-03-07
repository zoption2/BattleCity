using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShoot : Shoot
{
    private bool ready = false;
    protected override void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = gameObject.transform.GetChild(1);
        myBullets = new List<GameObject>();
        playersStats = PlayersStats.Instance;
        canShoot = true;
        timer = 0f;

        _soundManager = SoundManager.play;
    }

    public void GetReady(GameObject owner)
    {
        player = owner;
        _thisPlayer = player.tag;
        ready = true;
    }

    public override void AutoShoot()
    {
        if (ready && canShoot && Timer() <= 0)
        {
            var missile = TotalSpawner.spawn.SpawnFromSpawner("TurretBullet", target.position, Quaternion.LookRotation(transform.forward));
            var _missile = missile.GetComponent<Missile>();
            //_missile.SetShooter(player);
            _missile.SetTurretShooter(gameObject, _thisPlayer);
            SetMyBullet(missile);


            timer = StarsBoost.PrepareTime(_thisPlayer);
            if (StarsBoost.MultiShoot(_thisPlayer) == 5 && BulletListLength() < 2) canShoot = true;
            //else if (StarsBoost.MultiShoot(_thisPlayer) == 3 && BulletListLength() < 3) canShoot = true;
             else 
                canShoot = false;
            //_missile.isTurret = true;
            var shotVFX = TotalSpawner.spawn.SpawnFromSpawner("ShotVFX", target.transform.position, Quaternion.LookRotation(transform.forward));
            shotVFX.GetComponent<TailVFX>().LifeOfTail();
            _soundManager.PlayShortAudio("TankShoot", _soundWeapon, 0.3f, true);
        }
    }
}
