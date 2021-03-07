using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Missile
{
    private GameObject bulletTail;

    [SerializeField] private bool textureChange;
  
    private void OnEnable()
    {
        needCheckTexture = textureChange;
        speed = 15;
    }


    private void FixedUpdate()
    {
        _rigidbody.velocity = transform.forward * speed * StarsBoost.SpeedModifier(whoIsShooter);

        if (Physics.Linecast(transform.position, transform.position + transform.forward))
        {
            _collider.size = new Vector3(0.28f, 0.4f, 0.35f);
        }
        else _collider.size = new Vector3(0.28f, 0.4f, 0.2f);

        if (isActive)
        {
            bulletTail = TotalSpawner.spawn.SpawnFromSpawner("BulletTail", transform.position, transform.rotation);
            isActive = false;
        }
        
        bulletTail.transform.position = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        bulletTail.GetComponent<TailVFX>().LifeOfTail();

        if (collision.gameObject.GetComponent<Health>() != null)
        {
            var _health = collision.gameObject.GetComponent<Health>();
            _health.SetKillerName(whoIsShooter);
            _health.TakeDamage(1);
        }
        if (playerShotter != null) //playerShotter != null
        {
            var player = playerShotter.GetComponent<Shoot>();
            player.DeleteMyBullet(this.gameObject);
        }

        _collider.size = new Vector3(0.28f, 0.4f, 0.2f);
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tree" && MasterController.buffAxeActive[whoIsShooter] )
        {
            Destroy(other.gameObject);
        }
    }

    protected override void DoHitEffects()
    {
        vFX.PlayEffect("Hit", transform.position, Quaternion.identity, 0.69f);
       // hit.GetComponent<TailVFX>().LifeOfTail();
        if (playerShotter != null) soundManager.PlayShortAudio(soundManager.hitBricksSound(), 0.1f, true);


    }
}
