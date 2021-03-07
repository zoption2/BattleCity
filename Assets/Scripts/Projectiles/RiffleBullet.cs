using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiffleBullet : Missile
{
    private float localTimer;
    private void OnEnable()
    {
        localTimer = 0.2f;
    }
    private void LateUpdate()
    {
        localTimer -= Time.deltaTime;
        if (localTimer <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.GetComponent<Health>() != null)
        {
            var _health = collision.gameObject.GetComponent<Health>();
            _health.SetKillerName(whoIsShooter);
            _health.TakeDamage(0.5f);
        }
        //if (!isTurret && playerShotter != null) //playerShotter != null
        //{
        //    var player = playerShotter.GetComponent<Shoot>();
        //    player.DeleteMyBullet(this.gameObject);
        //}
        this.gameObject.SetActive(false);
    }

    protected override void DoHitEffects()
    {
        // hit effects
    }
}
