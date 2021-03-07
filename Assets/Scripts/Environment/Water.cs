using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private Collider waterCollider;

    private void Start()
    {
        waterCollider = GetComponentInChildren<Collider>();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (MasterController.buffAnchorActive.ContainsKey(other.gameObject.tag) && MasterController.buffAnchorActive[other.gameObject.tag])
        {
            Physics.IgnoreCollision(waterCollider, other.collider);
        }
        else return;
    }
}
