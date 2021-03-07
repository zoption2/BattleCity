using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassStrike : MonoBehaviour
{
    private float travelTime = 2f;
    private GameObject rocketTail;
    private float startTime;
    private Rigidbody _rigidbody;
    private Collider _collider;
    [HideInInspector] public GameObject enemy;
    [HideInInspector] public Vector3 _launcherPos;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }
    private void OnEnable()
    {
        startTime = Time.time;
    }
    private void OnDisable()
    {
        var hit = TotalSpawner.spawn.SpawnFromSpawner("Hit", transform.position, Quaternion.identity);
        hit?.GetComponent<TailVFX>().LifeOfTail();
    }

    private void FixedUpdate()
   {
        if (enemy == null)
        {
            rocketTail?.GetComponent<TailVFX>().LifeOfTail();
            this.gameObject.SetActive(false);
        }

        var targetRotation = Quaternion.LookRotation(enemy.transform.position - transform.position);
        _rigidbody.MoveRotation(targetRotation);
        var _thisTank = new Vector3(_launcherPos.x, _launcherPos.y + 2, _launcherPos.z);
        var centr = (enemy.transform.position + _thisTank) * 0.5f;
        centr -= new Vector3(0, 1, 0);
        Vector3 startAttCentr = _thisTank - centr;
        Vector3 endAttCentr = enemy.transform.position - centr;
        float fracComplete = (Time.time - startTime) / travelTime;

        transform.position = Vector3.Slerp(startAttCentr, endAttCentr, fracComplete);
        transform.position += centr;
   }

    private void OnCollisionEnter(Collision collision)
    {
        var collisionHealth = collision.gameObject.GetComponent<Health>();
        rocketTail?.GetComponent<TailVFX>().LifeOfTail();

        if (collision.gameObject.layer == 12)
        {
            collisionHealth.isSelfDestroy = true;
            collisionHealth.TakeDamage(4);
            this.gameObject.SetActive(false);
        }
        else
        {
            Physics.IgnoreCollision(_collider, collision.collider);
        }


    }
}
