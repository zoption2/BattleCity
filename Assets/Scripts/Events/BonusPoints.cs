using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class BonusPoints : MonoBehaviour
{
    [SerializeField] private GameObject Points;
    private static Queue<GameObject> objectSpawner;
    private int sizeOfPool = 10;

    private void Start()
    {
        NewGame();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            ShowPoints(500, new Vector3(0, 1.5f, 4));
        }
    }

    public static void ShowPoints(int points, Vector3 position)
    {
        GameObject objectToSpawn = objectSpawner.Dequeue();
        objectToSpawn.SetActive(true);
        TextMeshPro text = objectToSpawn.transform.Find("Canvas/Text").GetComponent<TextMeshPro>();
        text.text = points.ToString();
        objectToSpawn.transform.position = position;
        objectSpawner.Enqueue(objectToSpawn);
       
    }



    //private IEnumerator slideText()
    //{
    //    var currentPos = 0f;
    //    while (currentPos < slideDistance)
    //    {
    //       pos = new Vector3(pos.x, pos.y, pos.z + Time.deltaTime);
    //        _text.color = new Color(1, 1, 1, 1 - Time.deltaTime);
    //       currentPos += Time.deltaTime;
    //        yield return null;
    //    }
    //}

    //private void OnEnable()
    //{
    //    if (_text != null)
    //    {
    //        _text.color = new Color(1, 1, 1, 1);
    //        StartCoroutine(slideText());
    //    }
    //}

    private void NewGame()
    {
        GameObject PointsHolder = new GameObject("PointsPopUp");
        PointsHolder.transform.SetParent(this.gameObject.transform);
        objectSpawner = new Queue<GameObject>();
            for (int i = 0; i < sizeOfPool; i++)
            {
                GameObject obj = Instantiate(Points, PointsHolder.transform);
                obj.SetActive(false);
                objectSpawner.Enqueue(obj);
            }
    }
}


