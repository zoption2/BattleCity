using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ressurection : MonoBehaviour
{
    [SerializeField] private Image warning;
    [SerializeField] private Image respawn;
    public int _playerNumber;
    [HideInInspector] public float timeForRes;
    public string _playerName;

    private GameplayManager GPM;
    private int _totalPlayers;
    private float timer;
    private Coroutine currentCoroutine;


    private void Start()
    {
        _totalPlayers = MasterController.totalPlayersInGame;
        _playerName = "Player_" + _playerNumber;
        timeForRes = 5f;
        GPM = GameObject.Find("Canvas").GetComponent<GameplayManager>();
        warning.enabled = false;
        SetRespawn();
    }

    private void LateUpdate()
    {
        if (!MasterController.isPlayerAlive[_playerName] && _playerNumber <= _totalPlayers && MasterController.playerLives[_playerName] == 0)
        {
            SetWarning(true);
        }
        else SetWarning(false);

        if (warning.IsActive())
        warning.color = new Color(1,1,1,Mathf.PingPong(Time.time, 1));
    }

    public void SetWarning(bool isWarning)
    {
        warning.enabled = isWarning;
    }
    
    public void SetRespawn()
    {
        respawn.enabled = true;
        respawn.fillAmount = 0;
    }

    public void Respawn(float currentValue)
    {
        respawn.fillAmount = currentValue / timeForRes;
    }
    public void ResurrectPlayer(string saverName)
    {
        if ( !MasterController.isPlayerAlive[_playerName])
        {
            //MasterController.playerLives[saverName]--;
            //MasterController.playerLives[_playerName]++;
            PlayersStats.Instance.SetLives(saverName, -1);
            PlayersStats.Instance.SetLives(_playerName, 1);
            GPM.SpawnPlayer(_playerName);
        }
    }

    public void DoSpawning(string saverName)
    {
        StartCoroutine(spawning(saverName));
    }
    public void AbortSpawning()
    {
        SetRespawn();
        StopAllCoroutines();
    }
    private IEnumerator spawning(string saverName)
    {
        SetRespawn();
        timer = 0;
        while (timer < timeForRes)
        {
            timer += Time.deltaTime;
            Respawn(timer);
            yield return null;
        }
        ResurrectPlayer(saverName);
        SetWarning(false);
        SetRespawn();
    }

}
