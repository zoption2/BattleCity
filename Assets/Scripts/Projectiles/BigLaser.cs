using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigLaser : Missile
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LayerMask layerMaskforBorder;

    private RaycastHit hit;
    private RaycastHit[] detectedHits;

    public void DoRayShoot()
    {
        StartCoroutine(RayShoot());
    }
    private IEnumerator RayShoot()
    {
        // if (Physics.Raycast(transform.position, transform.forward, out hit, 50, layerMask))
        if (Physics.BoxCast(transform.position, new Vector3(0.1f, 0.1f, 0.1f), transform.forward, out hit, Quaternion.identity, 50f, layerMaskforBorder))
        {
            detectedHits = Physics.BoxCastAll(transform.position, new Vector3(0.8f, 0.4f, 0.8f), transform.forward, Quaternion.identity, 50f, layerMask);
            var startPos = transform.position;
            var endPos = hit.point;

            _collider.enabled = true;
            _lineRenderer.enabled = true;
            float laserTime = 0f;

            StartCoroutine(CheckMassCollision());

            while (laserTime <= 0.4f)
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

    private IEnumerator CheckMassCollision()
    {
        var checkedPoints = 0;
        while (checkedPoints < detectedHits.Length)
        {
            foreach (var collision in detectedHits)
            {
                var GO = collision.transform.gameObject;
                if (GO != null)
                {
                    if (GO.tag == "Base")
                    {
                        break;
                    }

                    if (GO.gameObject.GetComponent<Health>() != null)
                    {
                        var _health = GO.gameObject.GetComponent<Health>();
                        _health.SetKillerName(whoIsShooter);
                        _health.IsBigGun = true;
                        _health.TakeDamage(5);
                    }
                }
                 
                checkedPoints++;
            }
            yield return null;
        }

        _collider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tree" && MasterController.buffAxeActive[whoIsShooter])
        {
            Destroy(other.gameObject);
        }
    }

    protected override void DoHitEffects()
    {
        //var hit = TotalSpawner.spawn.SpawnFromSpawner("Hit", transform.position, Quaternion.identity);
        //hit.GetComponent<TailVFX>().LifeOfTail();
        //soundManager?.PlayShortAudio(soundManager.hitBricksSound(), 0.1f, true);
    }
}
