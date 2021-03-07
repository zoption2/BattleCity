using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpawnPlacer : MonoBehaviour
{
    private Collider _collider;


    public void CheckPosition(LayerMask destroyeble)
    {
        _collider = GetComponent<Collider>();
        Vector3 myPosition = transform.position;
        Vector3 halfSize = _collider.bounds.extents * 1.5f;

        //найти все объекты, которые мешают нашим стартовым спаунерам, базе и убераем их с уровня
        var findObjects = Physics.BoxCastAll(myPosition, halfSize, myPosition, Quaternion.identity, 0, destroyeble);

        foreach (var item in findObjects)
        {
            item.collider.gameObject.SetActive(false);
        }


        //Transform[] myTransforms = GetComponentsInChildren<Transform>();
        //int count = myTransforms.Length;
        //for (int i = 0; i < count; i++)
        //{
        //    myTransforms[i].gameObject.SetActive(true);
        //}
 
    }
}
