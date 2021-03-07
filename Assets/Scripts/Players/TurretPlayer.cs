using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPlayer : MonoBehaviour
{
    public GameObject player;
    public static float lifeTime;

    private Transform target;
    private string _thisPlayer;
    private Renderer [] renderer;
    private Texture perentTexture;
    private SoundManager _soundManager;
    private TurretShoot _shoot;
    protected List<GameObject> myBullets;
    private int modif;
    private string myName;
    private enum StateOfTurret {shooting, standing}
    private StateOfTurret stateOfTurret;
    private GameplayManager GPM;


    public void GetStarted()
    {
        _thisPlayer = player.gameObject.tag;
        target = transform.GetChild(1);
        renderer = GetComponentsInChildren<Renderer>();
       // lifeTime = GetTimer();
        _soundManager = SoundManager.play;
        _shoot = GetComponent<TurretShoot>();
        _shoot.GetReady(player);
        // StartCoroutine(CheckTexture());
        modif = Random.Range(1, 9999);
        myName = gameObject.tag + modif.ToString();
        MasterController.activePlayers.Add(myName, this.gameObject);

        GPM = GameObject.Find("Canvas").GetComponent<GameplayManager>();
        GPM.OnLevelCompleated += LevelCompleateAction;
    }

    private IEnumerator LifeofTurret()
    {
        yield return new WaitForSeconds(lifeTime);
        MasterController.activePlayers.Remove(myName);
        Destroy(this.gameObject);
    }
    private void Update()
    {
        switch (stateOfTurret)
        {
            case StateOfTurret.shooting:
                _shoot.AutoShoot();
                break;
            case StateOfTurret.standing:
                break;
        }
    }

    private void LevelCompleateAction()
    {
        stateOfTurret = StateOfTurret.standing;
    }
  
    private float GetTimer()
    {
        switch (MasterController.playerStars[_thisPlayer])
        {
            case 0: return 15f;
            case 1: return 16.5f;
            case 2: return 18f;
            case 3: return 19.5f;
            case 4: return 21f;
            case 5: return 23.5f;
            default: return 23.5f;
        }
    }

    private float GetReload()
    {
        switch (MasterController.playerStars[_thisPlayer])
        {
            case 0: return 1.5f;
            case 1: return 1.4f;
            case 2: return 1.3f;
            case 3: return 1.2f;
            case 4: return 1.1f;
            case 5: return 1f;
            default: return 1f;
        }
    }
    public void DestroyTurret()
    {
        Destroy(this.gameObject);
    }

    private IEnumerator CheckTexture()
    {
        while (player == null)
        {
            yield return null;
        }
        CheckTextures();
    }
    private void CheckTextures()
    {
        perentTexture = player.GetComponentInChildren<Renderer>().material.mainTexture;
        foreach (var item in renderer)
        {
            item.material.mainTexture = perentTexture;
        }
    }

    private void OnDisable()
    {
        GPM.OnLevelCompleated -= LevelCompleateAction;
        MasterController.activePlayers?.Remove(myName);
        Destroy(this.gameObject);
    }
}
