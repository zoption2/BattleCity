using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIEventAgent : MonoBehaviour
{
    private PlayersStats playersStats;

    private PlayersUI playersUI;


    private void Start()
    {
        playersStats = PlayersStats.Instance;
        //playersUI = GetComponent<PlayersUI>();
        playersUI = PlayersUI.instance;
        playersStats.OnLivesChanged += SetLivesUI;
        playersStats.OnStarsChanged += SetStarsUI;
        playersStats.OnProjectilesChanged += SetProjectilesUI;
        playersStats.OnBuffStars += SetBuffStarsUI;
    }
    private void OnDestroy()
    {
        playersStats.OnLivesChanged -= SetLivesUI;
        playersStats.OnStarsChanged -= SetStarsUI;
        playersStats.OnProjectilesChanged -= SetProjectilesUI;
        playersStats.OnBuffStars -= SetBuffStarsUI;
    }
    private void SetLivesUI(string name)
    {
        //var name = playersStats.PlayerName;
        playersUI.UpdateLifeInfo(name, playersStats.GetLives(name));
    }

    private void SetStarsUI(string name)
    {
        //var name = playersStats.PlayerName;
        playersUI.UpdateStarInfo(name, playersStats.GetStars(name));
    }

    private void SetProjectilesUI(string name)
    {
        //var name = playersStats.PlayerName;
        playersUI.UpdateProjectileInfo(name, playersStats.GetProjectiles(name));
    }

    private void SetBuffStarsUI(string name)
    {
        //var name = playersStats.PlayerName;
        playersUI.BuffStars(name, playersStats.GetStars(name));
    }
}
