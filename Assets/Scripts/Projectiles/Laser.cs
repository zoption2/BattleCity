using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Missile
{
    [SerializeField] private LayerMask layerMask;
    private RaycastHit hit;

    public void DoRayShoot()
    {
        StartCoroutine(RayShoot());
    }
    private IEnumerator RayShoot()
    {
        // if (Physics.Raycast(transform.position, transform.forward, out hit, 50, layerMask))
        if (Physics.BoxCast(transform.position, new Vector3(0.1f, 0.1f, 0.1f), transform.forward, out hit, Quaternion.identity, 50f, layerMask))  
       {
            var startPos = transform.position;
            var endPos = hit.point;
            _collider.enabled = true;
            _lineRenderer.enabled = true;
            float laserTime = 0f;
           
            while (laserTime <= 0.2f)
            {
                laserTime += Time.deltaTime;
                _lineRenderer.SetPosition(0, startPos);
                _lineRenderer.SetPosition(1, endPos);
                transform.position = endPos;
                yield return null;
            }
            _lineRenderer.enabled = false;
            this.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Health>() != null)
        {
            var _health = collision.gameObject.GetComponent<Health>();
            var damageModifier = PlayersStats.Instance.GetStars(whoIsShooter);
            _health.SetKillerName(whoIsShooter);
            _health.TakeDamage(Mathf.Clamp(1 * damageModifier, 1, 5));
        }
        _collider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tree" && MasterController.buffAxeActive[playerShotter.tag])
        {
            Destroy(other.gameObject);
        }
    }

    protected override void DoHitEffects()
    {
        vFX.PlayEffect("HitBlast", transform.position, Quaternion.identity, 0.7f);
        //var hit = TotalSpawner.spawn.SpawnFromSpawner("Hit", transform.position, Quaternion.identity);
        //hit.GetComponent<TailVFX>().LifeOfTail();
        //soundManager?.PlayShortAudio(soundManager.hitBricksSound(), 0.1f, true);
    }
}
