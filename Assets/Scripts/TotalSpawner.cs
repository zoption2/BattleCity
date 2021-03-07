using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalSpawner : MonoBehaviour, I_DontDestroy
{
    [System.Serializable]
    public class Spawner
    {
        public string tag;
        public GameObject prefab;
        public int size;


        public Spawner(string tagOfSpawnObject, GameObject prefabOfObject, int sizeOfPool)
        {
            this.tag = tagOfSpawnObject;
            this.prefab = prefabOfObject;
            this.size = sizeOfPool;
        }
    }

    #region Singleton
    public static TotalSpawner spawn;
    private void Awake()
    {
        if (spawn == null)
        {
            spawn = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
        
    }
    #endregion


    public List<Spawner> spawners;

    public Dictionary<string, Queue<GameObject>> poolDictionary;


    private void Start()
    {
        //GlobalWatcher.instance.AddDontDestroeble(this);
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Spawner spawner in spawners)
        {
            GameObject newOne = new GameObject("Pool" + spawner.tag);
            newOne.transform.SetParent(this.gameObject.transform);
            Queue<GameObject> objectSpawner = new Queue<GameObject>();
            for (int i = 0; i < spawner.size; i++)
            {
                GameObject obj = Instantiate(spawner.prefab, newOne.transform);
                obj.SetActive(false);
                objectSpawner.Enqueue(obj);
            }

            poolDictionary.Add(spawner.tag, objectSpawner);
        }
    }



    public GameObject SpawnFromSpawner(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.Log("Спаунера " + tag + " не существует!");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;


        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public GameObject ReturnGameObject()
    {
        return this.gameObject;
    }
}

