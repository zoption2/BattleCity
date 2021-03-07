using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBoosterUI : MonoBehaviour
{
    [SerializeField] private GameObject _firstBooster;
    [SerializeField] private GameObject _secondBooster;
    [SerializeField] private GameObject _thirdBooster;

    private string _playerName;
    private PlayersStats playersStats;

    private void Start()
    {
        _playerName = transform.parent.tag;
        playersStats = PlayersStats.Instance;
        //playersStats.OnProjectilesChanged += UIUpdate;
        //UIUpdate();
    }
    //private void OnDisable()
    //{
    //    playersStats.OnProjectilesChanged -= UIUpdate;
    //}
    private void LateUpdate()
    {
        //if (_playerName == playersStats.PlayerName)
        //{
            if (MasterController.isPlayerBoosterUI[_playerName])
            {
                switch (MasterController.playerBoosters[_playerName])
                {
                    case 1:
                        _firstBooster.SetActive(true);
                        _secondBooster.SetActive(false);
                        _thirdBooster.SetActive(false);
                        break;
                    case 2:
                        _firstBooster.SetActive(true);
                        _secondBooster.SetActive(true);
                        _thirdBooster.SetActive(false);
                        break;
                    case 3:
                        _firstBooster.SetActive(true);
                        _secondBooster.SetActive(true);
                        _thirdBooster.SetActive(true);
                        break;
                    default:
                        _firstBooster.SetActive(false);
                        _secondBooster.SetActive(false);
                        _thirdBooster.SetActive(false);
                        break;
                }
            }
            else
            {
                _firstBooster.SetActive(false);
                _secondBooster.SetActive(false);
                _thirdBooster.SetActive(false);
            }
        //}
       
    }
}
