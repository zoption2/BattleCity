using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterMissileHit : MonoBehaviour
{
    private ParticleSystem missileHit;
    private bool isWork;
    private void Awake()
    {
        missileHit = GetComponent<ParticleSystem>();
        isWork = false;
    }
    private void OnEnable()
    {
        isWork = true;
        StartCoroutine("VFXplay");
    }
    private IEnumerator VFXplay()
    {
        if (isWork == true)
        {
            isWork = false;
            missileHit.Play();
            yield return new WaitForSeconds(0.8f);
            gameObject.SetActive(false);


        }
    }
}
