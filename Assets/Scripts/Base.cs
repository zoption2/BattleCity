using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    void OnEnable()
    {
        MasterController.activePlayers.Add(gameObject.tag, this.gameObject);
    }
    private void OnDisable()
    {
        MasterController.activePlayers.Remove(gameObject.tag);
    }

}
