using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootRiffle : Shoot
{
    private RaycastHit hit;
    private bool canShoot = true;
    private GameObject riffleGun;
    private ParticleSystem riffleGunShot;
    private LayerMask layerMask;
    private BoxCollider collider;
    private RaycastHit[] raycastHits;
    private PlayersStats playersStats;
    private int starCount = 0;
    private float Timer;

    protected override void Start()
    {
        base.Start();
        var conteinerRiffleGun = PrefabsConteiner.instance.riffleGun;
        riffleGun = Instantiate(conteinerRiffleGun, transform.GetChild(1));
        riffleGunShot = riffleGun.GetComponent<ParticleSystem>();
        playersStats = PlayersStats.Instance;

        playersStats.OnStarsChanged += SetStarCount;
        starCount = playersStats.GetStars(_thisPlayer);

        layerMask = LayerMask.GetMask("RedWall", "SteelWall", "Border", "Enemy", "EnemyProjectile");
    }

    private void OnDisable()
    {
        playersStats.OnStarsChanged -= SetStarCount;
    }

    public override void AutoShoot()
    {
        StartCoroutine(riffleWork());
    }

    private void SetStarCount(string name)
    {
        //var name = playersStats.PlayerName;
        if (name == _thisPlayer)
        {
            starCount = playersStats.GetStars(name);
        }

    }
    private float maxDistance
    {
        get
        {
            switch (starCount)
            {
                case 1: return 12;
                case 2: return 14;
                case 3: return 16;
                case 4: return 18;
                case 5: return 20;
                default: return 10;
            }
        }
    }

    private float shootTimer
    {
        get
        {
            switch (starCount)
            {
                case 1: return 0.35f;
                case 2: return 0.3f;
                case 3: return 0.25f;
                case 4: return 0.2f;
                case 5: return 0.15f;
                default: return 0.4f;
            }
        }
    }

    private IEnumerator riffleWork()
    {
        if (canShoot)
        {
            canShoot = false;
            Timer = shootTimer;

            if (Physics.BoxCast(target.transform.position, new Vector3(0.8f, 0.8f, 0.8f), transform.forward, out hit, Quaternion.identity, maxDistance, layerMask))
            {
                var hitPointDistance = hit.distance + 0.5f;
                var hitPoint = target.position + (target.forward * hitPointDistance);
                var rotation = Quaternion.Euler(90, Random.Range(-45, 45), 0);
                var bullet = TotalSpawner.spawn.SpawnFromSpawner("RiffleBullet", hitPoint, Quaternion.identity);
                RectTransform rectTransform = bullet.GetComponentInChildren<RectTransform>();
                rectTransform.rotation = rotation;
                Missile mis = bullet.GetComponent<Missile>();
                mis.SetShooter(player);
            }
            riffleGunShot.Play();
            while (Timer > 0)
            {
                Timer -= Time.deltaTime;
                yield return null;
            }
            riffleGunShot.Stop();
            canShoot = true;
        }
       
    }
}
