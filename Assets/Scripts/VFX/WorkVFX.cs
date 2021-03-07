using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkVFX : MonoBehaviour
{
    [SerializeField] private float timeOfVFX;
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        StartCoroutine("work"); 
    }

    private IEnumerator work()
    {
        if (!_particleSystem.isPlaying)
        {
            _particleSystem.Play();
        }
        
        yield return new WaitForSeconds(timeOfVFX);
        this.gameObject.SetActive(false);
    }
}
