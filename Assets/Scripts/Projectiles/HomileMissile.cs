using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomileMissile : Missile
{
    private float travelTime = 2f;
    private GameObject rocketTail;
    private float startTime;
     public GameObject enemy;
    [HideInInspector] public GameObject thisTank;
    [SerializeField] private bool textureChange;

    private void OnEnable()
    {
        needCheckTexture = textureChange;
        startTime = Time.time;
    }
    private void FixedUpdate()
    {
        if (enemy == null)
        {
            rocketTail.GetComponent<TailVFX>().LifeOfTail();
            this.gameObject.SetActive(false);
        }

        var targetRotation = Quaternion.LookRotation(enemy.transform.position - transform.position);
        _rigidbody.MoveRotation(targetRotation);
        var _thisTank = new Vector3(playerShotter.transform.position.x, playerShotter.transform.position.y + 2, playerShotter.transform.position.z);
        var centr = (enemy.transform.position + _thisTank) * 0.5f;
        centr -= new Vector3(0, 1, 0);
        Vector3 startAttCentr = _thisTank - centr;
        Vector3 endAttCentr = enemy.transform.position - centr;
        float fracComplete = (Time.time - startTime) / travelTime;

        transform.position = Vector3.Slerp(startAttCentr, endAttCentr, fracComplete);
        transform.position += centr;

        if (isActive)
        {
            rocketTail = TotalSpawner.spawn.SpawnFromSpawner("RocketVFX", transform.position, transform.rotation);
            isActive = false;
        }

        rocketTail.transform.position = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var collisionHealth = collision.gameObject.GetComponent<Health>();
        rocketTail.GetComponent<TailVFX>().LifeOfTail();

        if (collision.gameObject.layer == 12)
        {
            collisionHealth.SetKillerName(whoIsShooter);
            collisionHealth.TakeDamage(1);
            this.gameObject.SetActive(false);
        }
        else
        {
            Physics.IgnoreCollision(_collider, collision.collider);
        }
 
        
    }

    protected override void DoHitEffects()
    {
        vFX.PlayEffect("Hit", transform.position, Quaternion.identity, 0.69f);
        soundManager?.PlayShortAudio(soundManager.hitBricksSound(), 0.1f, true);
    }
}
