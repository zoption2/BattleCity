using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDealer : MonoBehaviour
{
    private Renderer [] renderer;
    private Material buffDealer;
    private string currentBuff;


    private void Awake()
    {
        renderer = GetComponentsInChildren<Renderer>();
        buffDealer = Resources.Load("Materials/BuffDealler") as Material;
        foreach (var item in renderer)
        {
            item.material = buffDealer;
        }
    }

    private int newCoord()
    {
        int coord = Random.Range(1, 29);
        return coord;
    }

    //private void OnCollisionEnter(Collision collider)
    //{
    //    if (collider.gameObject.GetComponent<Missile>() != null)
    //    {
    //        List<Buffs> buffBox = new List<Buffs>();
    //        var buffList = FindObjectsOfType<Buffs>();
    //        buffBox.AddRange(buffList);

    //            foreach (var buff in buffBox)
    //            {
    //                buff.gameObject.SetActive(false);
    //            }
    //        buffBox.Clear();

            
    //        currentBuff = MasterController.buffs[Random.Range(0, MasterController.buffs.Count)];
    //        TotalSpawner.spawn.SpawnFromSpawner(currentBuff, new Vector3(newCoord(), transform.position.y + 1.1f, newCoord()), Quaternion.Euler(0,180,0));
    //    }
    //}

    private void SpawnOnDeath()
    {
        List<Buffs> buffBox = new List<Buffs>();
        var buffList = FindObjectsOfType<Buffs>();
        buffBox.AddRange(buffList);

        foreach (var buff in buffBox)
        {
            buff.gameObject.SetActive(false);
        }
        buffBox.Clear();


        currentBuff = MasterController.buffs[Random.Range(0, MasterController.buffs.Count)];
        TotalSpawner.spawn.SpawnFromSpawner(currentBuff, new Vector3(newCoord(), transform.position.y + 1.1f, newCoord()), Quaternion.Euler(0, 180, 0));
    }


    private void OnDisable()
    {
        Assistent.assist.SummonBuff();
        Destroy(this);
    }
}
