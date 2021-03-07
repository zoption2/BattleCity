using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_DontDestroy 
{
    GameObject ReturnGameObject();
}
public class GlobalWatcher : MonoBehaviour
{
    public static GlobalWatcher instance;
    private List<I_DontDestroy> dontDestroyList = new List<I_DontDestroy>();

    [SerializeField] private GameObject _multiplayerEvent;
    private void Awake()
    {
        DestroyOnLoad();
        MasterController.StartNewGame();
        Conteiner.ReloudConteiner();
        PrepareForGame.DoNewGame();
        Instantiate(_multiplayerEvent);

        if (instance == null)
        {
            instance = this;
        }
        else Destroy(this.gameObject);
    }

    public void AddDontDestroeble(I_DontDestroy whatDontDestroy)
    {
        dontDestroyList.Add(whatDontDestroy);
    }
    public void DestroyOnLoad()
    {
        var toDelete = FindObjectsOfType<DeleteOnLoad>();
        foreach (var item in toDelete)
        {
            Destroy(item.gameObject);
        }
        //foreach (var item in dontDestroyList)
        //{
        //    Destroy(item.ReturnGameObject());
        //    dontDestroyList.Remove(item);
        //}
    }
}
