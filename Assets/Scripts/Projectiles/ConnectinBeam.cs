using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectinBeam : Missile
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
           // _collider.enabled = true;
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

    protected override void DoHitEffects()
    {
        //var hit = TotalSpawner.spawn.SpawnFromSpawner("Hit", transform.position, Quaternion.identity);
        //hit.GetComponent<TailVFX>().LifeOfTail();
        //soundManager?.PlayShortAudio(soundManager.hitBricksSound(), 0.1f, true);
    }
}
