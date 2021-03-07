using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : Shoot
{
    public override void AutoShoot()
    {
        if (canShoot && Timer() == 0)
        {
            GameObject missile;
            animator.SetTrigger("isShoot");
            missile = (GameObject)TotalSpawner.spawn.SpawnFromSpawner("MissilePlayer" + playerNumber, target.transform.position, Quaternion.LookRotation(transform.forward));
            var _missile = missile.GetComponent<Missile>();
            _missile.SetShooter(this.gameObject);
            SetMyBullet(missile);


            timer = StarsBoost.PrepareTime(_thisPlayer);
            if (StarsBoost.MultiShoot(_thisPlayer) == 2 && BulletListLength() < 2) canShoot = true;
            else if (StarsBoost.MultiShoot(_thisPlayer) == 3 && BulletListLength() < 3) canShoot = true;
            else canShoot = false;
            var shotVFX = TotalSpawner.spawn.SpawnFromSpawner("ShotVFX", target.transform.position, Quaternion.LookRotation(transform.forward));
            shotVFX.GetComponent<TailVFX>().LifeOfTail();
            _soundManager.PlayShortAudio(_shootSound, _soundWeapon, 0.2f, true);
        }
    }
}
