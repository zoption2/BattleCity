using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantedBomb : Missile
{
    [SerializeField] private bool textureChange;
    private void OnEnable()
    {
        needCheckTexture = textureChange;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Health>() != null)
        {
            collision.gameObject.GetComponent<Health>().SetKillerName(whoIsShooter);
            collision.gameObject.GetComponent<Health>().TakeDamage(2);
            this.gameObject.SetActive(false);
        }




    }
    protected override void DoHitEffects()
    {
        vFX.PlayEffect("Hit", transform.position, Quaternion.identity, 0.69f);
        soundManager?.PlayShortAudio(soundManager.hitBricksSound(), 0.1f, true);
    }
}
