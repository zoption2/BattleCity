using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NucleMissile : Missile
{
    [SerializeField] private bool textureChange;
    private void OnEnable()
    {
        needCheckTexture = textureChange;
    }
    private void FixedUpdate()
    {
        _rigidbody.velocity = transform.forward * 10;
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "SteelWall":
                collision.gameObject.SetActive(false);
                break;
            case "RedWall":
                collision.gameObject.SetActive(false);
                break;
            case "BorderWall":
                this.gameObject.SetActive(false);
                break;
            case "Base":
               // collision.gameObject.GetComponent<Health>().GetKillerName(MasterController.whoIsShooter[this.gameObject.GetInstanceID()]);
                collision.gameObject.GetComponent<Health>().TakeDamage(1);
                break;

            default:
                if (collision.gameObject.GetComponent<Health>() != null)
                {
                    //collision.gameObject.GetComponent<Health>().GetKillerName(MasterController.whoIsShooter[this.gameObject.GetInstanceID()]);
                    collision.gameObject.GetComponent<Health>().TakeDamage(2);
                    Physics.IgnoreCollision(this.gameObject.GetComponentInChildren<Collider>(), collision.collider, true);
                }
                else this.gameObject.SetActive(false);
                break;
        }
  
    }

    protected override void DoHitEffects()
    {

    }
}
