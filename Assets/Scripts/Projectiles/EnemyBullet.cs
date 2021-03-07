using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Missile
{
    private GameObject bulletTail;
    private BoxCollider boxCollider;
    private void OnEnable()
    {
        StartCoroutine(CheckEnemy());
        isActive = true;
        boxCollider = GetComponentInChildren<BoxCollider>();
    }
    private void FixedUpdate()
    {
        _rigidbody.velocity = transform.forward * speed;

        if (Physics.Linecast(transform.position, transform.position + transform.forward))
        {
            boxCollider.size = new Vector3(0.28f, 0.4f, 0.35f);
        }
        else _collider.size = new Vector3(0.28f, 0.4f, 0.2f);

        if (isActive)
        {
            bulletTail = TotalSpawner.spawn.SpawnFromSpawner("BulletTail", transform.position, transform.rotation);
            isActive = false;
        }

        bulletTail.transform.position = transform.position;
    }

    private IEnumerator CheckEnemy()
    {
        while (whoIsShooter == null)
        {
            yield return null;
        }
        ChooseOfEnemy();
    }
    private void ChooseOfEnemy()
    {
        switch (whoIsShooter)
        {
            case "BigEnemy": speed = 25f;
                break;
            case "SmallEnemy": speed = 15f;
                break;
            case "FastEnemy": speed = 15f;
                break;
            default: speed = 15f;
                break;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        bulletTail.GetComponent<TailVFX>().LifeOfTail();

        if (collision.gameObject.GetComponent<Health>() != null && collision.gameObject.tag != "SteelWall")
        {
            var _health = collision.gameObject.GetComponent<Health>();
            _health.SetKillerName(whoIsShooter);
            _health.TakeDamage(1);
        }

        if (playerShotter)
        {
            var AI = playerShotter.GetComponent<EnemyAI>();
            AI.canShoot = true;
        }



        boxCollider.size = new Vector3(0.28f, 0.4f, 0.2f);
        this.gameObject.SetActive(false);
    }

    protected override void DoHitEffects()
    {
        vFX.PlayEffect("Hit", transform.position, Quaternion.identity, 0.69f);
        soundManager.PlayShortAudio(soundManager.hitBricksSound(), 0.1f, true);
    }
}
