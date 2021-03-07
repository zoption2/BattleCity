using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : Missile
{
    public Player player;


    private IEnumerator collisionSpeedModifier(float time, float modifier)
    {
        player.SetSpeed(true, modifier);
        yield return new WaitForSeconds(time);
        player.SetSpeed(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Base")
        {
            return;
        }

        if (collision.gameObject.GetComponent<Health>() != null)
        {
            var _health = collision.gameObject.GetComponent<Health>();
            _health.SetKillerName(whoIsShooter);
            _health.TakeDamage(5);
            StartCoroutine(collisionSpeedModifier(0.5f, 0.7f));
        }
        //if (!isTurret && playerShotter != null) //playerShotter != null
        //{
        //    var player = playerShotter.GetComponent<Shoot>();
        //    player.DeleteMyBullet(this.gameObject);
        //}

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
        if (playerShotter != null) soundManager.PlayShortAudio(soundManager.hitBricksSound(), 0.1f, true);
    }
}
