using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailVFX : MonoBehaviour
{
    [SerializeField] private float time = 1;
       
    public void LifeOfTail()
    {
        StartCoroutine(lifeOfTail());
    }
    private IEnumerator lifeOfTail()
    {
        yield return new WaitForSeconds(time);
        this.gameObject.SetActive(false);
    }
}
