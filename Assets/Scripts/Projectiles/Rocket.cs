using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Missile
{
    private GameObject rocketTail;
    [SerializeField] private bool textureChange;
    private void OnEnable()
    {
        needCheckTexture = textureChange;
    }
    private void FixedUpdate()
    {
        _rigidbody.velocity = transform.forward * 30;

        if (isActive)
        {
            rocketTail = TotalSpawner.spawn.SpawnFromSpawner("RocketVFX", transform.position, transform.rotation);
            isActive = false;
        }

        rocketTail.transform.position = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        rocketTail.GetComponent<TailVFX>().LifeOfTail();

        switch (collision.gameObject.tag)
        {
            case "SteelWall":
                collision.gameObject.GetComponent<Health>().TakeDamage(3);
                    break;
            case "RedWall":
                collision.gameObject.SetActive(false);
                break;
            case "BorderWall":
                break;
            case "Base":
                //collision.gameObject.GetComponent<Health>().GetKillerName(MasterController.whoIsShooter[this.gameObject.GetInstanceID()]);
                collision.gameObject.GetComponent<Health>().TakeDamage(1);
                break;
            
            default:
                if (collision.gameObject.GetComponent<Health>() != null)
                {
                    //collision.gameObject.GetComponent<Health>().GetKillerName(MasterController.whoIsShooter[this.gameObject.GetInstanceID()]);
                    collision.gameObject.GetComponent<Health>().TakeDamage(1);
                }
                break;
        }

            this.gameObject.SetActive(false);
    }

    protected override void DoHitEffects()
    {
        var hit = TotalSpawner.spawn.SpawnFromSpawner("Hit", transform.position, Quaternion.identity);
        hit.GetComponent<TailVFX>().LifeOfTail();
        soundManager?.PlayShortAudio(soundManager.hitBricksSound(), 0.1f, true);
    }
}
