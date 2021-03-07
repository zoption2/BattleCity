using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXTotalSpawner : MonoBehaviour
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
    public static VFXTotalSpawner Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
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
            GameObject VFXHolder = new GameObject("VFX" + spawner.tag);
            VFXHolder.transform.SetParent(this.gameObject.transform);
            Queue<GameObject> objectSpawner = new Queue<GameObject>();
            for (int i = 0; i < spawner.size; i++)
            {
                GameObject obj = Instantiate(spawner.prefab, VFXHolder.transform);
               // obj.SetActive(false);
                objectSpawner.Enqueue(obj);
            }

            poolDictionary.Add(spawner.tag, objectSpawner);
        }
    }


    /// <summary>
    /// Указывать Transform parent если эффект должен двигаться за другим обьектом, пока существует.
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public void PlayEffect(string tag, Vector3 position, Quaternion rotation, float workTime = 1f, Transform parent = null)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.Log("VFX спаунера " + tag + " не существует!");
            return;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
       

       // objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;


        poolDictionary[tag].Enqueue(objectToSpawn);


        StartCoroutine(Effect(objectToSpawn, workTime, parent));
    }


    private IEnumerator EffectEmit(GameObject VFXobject, float workTimeVFX, Transform parent)
    {
        ParticleSystem _particleSystem;
        bool hasChildren;
        float timer = workTimeVFX;
        _particleSystem = VFXobject.GetComponent<ParticleSystem>();

        if (VFXobject.transform.childCount > 0)
        {
            hasChildren = true;
        }
        else hasChildren = false;

        _particleSystem.Play(hasChildren);
        while (timer > 0)
        {
            if (parent != null)
            {
                VFXobject.transform.position = new Vector3(parent.position.x, VFXobject.transform.position.y, parent.position.z);
            }


            timer -= Time.deltaTime;
            yield return null;
        }
        _particleSystem.Stop(hasChildren);

        //VFXobject.transform.SetParent(myStartFolder);
        // VFXobject.SetActive(false);
    }
    private IEnumerator Effect(GameObject VFXobject, float workTimeVFX, Transform parent)
    {
        ParticleSystem _particleSystem;
        bool hasChildren;
        float timer = workTimeVFX;
        _particleSystem = VFXobject.GetComponent<ParticleSystem>();

        if (VFXobject.transform.childCount > 0)
        {
            hasChildren = true;
        }
        else hasChildren = false;

        _particleSystem.Play(hasChildren);
        while (timer > 0)
        {
            if (parent != null)
            {
                VFXobject.transform.position = new Vector3(parent.position.x, VFXobject.transform.position.y, parent.position.z);
            }


            timer -= Time.deltaTime;
            yield return null;
        }
        _particleSystem.Stop(hasChildren);

        //VFXobject.transform.SetParent(myStartFolder);
       // VFXobject.SetActive(false);
    }

    public void AddPermanentEffect(string tag, string holderName, GameObject origin)
    {
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        poolDictionary[tag].Enqueue(objectToSpawn);
        Transform holder;
        holder = origin.transform.Find(holderName);
       
        if (holder != null)
        {
            var subHolder = holder.GetComponentsInChildren<Transform>();
            foreach (var item in subHolder)
            {
                Destroy(item.gameObject);
            }
        }

        holder = new GameObject(holderName).transform;
        holder.SetParent(origin.transform);
        holder.localPosition = new Vector3(0, 0, 0);
        
        var insta = Instantiate(objectToSpawn, holder);
        insta.transform.localPosition = new Vector3(0,0,0);
    }

    public void DestroyPermanentEffect(string holderName, GameObject origin)
    {
        if (origin.transform.Find(holderName))
        {
            var holder = origin.transform.Find(holderName);
            Destroy(holder);
        }
    }

}
