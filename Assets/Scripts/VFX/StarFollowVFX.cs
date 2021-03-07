using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarFollowVFX : MonoBehaviour
{
    public void FollowPlayer(string playerTag)
    {
        StartCoroutine(followPlayer(playerTag));
    }
private IEnumerator followPlayer(string playerTag)
    {
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);
        while (this.gameObject.activeSelf)
        {
            transform.position = player.transform.position;
            yield return null;
        }
    }
}
