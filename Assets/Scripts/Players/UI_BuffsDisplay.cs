using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum StateOfCircle { attackCircle, defenseCircle }
public class UI_BuffsDisplay : MonoBehaviour
{
    private Transform extraLifeCircle;
    private Transform attackCircle;
    private Transform defendCircle;
    private Transform activeCircle;

    private string _playerName;
    private Player player;
    private PlayersStats playersStats;
    private VFXTotalSpawner vFXTotalSpawner;
    //public StateOfCircle stateOfCircle;

    public UnityAction OnDisableTank;

    private void Start()
    {
        extraLifeCircle = transform.Find("Circle_ExtraLife");
        attackCircle = transform.Find("Circle_Attack");
        defendCircle = transform.Find("Circle_Defend");
        _playerName = gameObject.tag;
        player = GetComponent<Player>();
        playersStats = PlayersStats.Instance;
        vFXTotalSpawner = VFXTotalSpawner.Instance;
        playersStats.OnHealthChange += ShowExtraLifeCircle;
        ShowExtraLifeCircle(_playerName);
        SetSpeedVFX(MasterController.speedBoosts[_playerName]);
        player.OnSpeedBoostChange += SetSpeedVFX;
    }

    public void ShowExtraLifeCircle(string name)
    {
        if (name == _playerName)
        {
            var lives = playersStats.GetHealth(name);

            switch (lives)
            {
                case 2:
                    extraLifeCircle.gameObject.SetActive(true);
                    break;
                default:
                    extraLifeCircle.gameObject.SetActive(false);
                    break;
            }
        }
    }

    //public void ShowCircle()
    //{
    //    attackCircle.gameObject.SetActive(true);
    //}
    public void HideCircle()
    {
        attackCircle.gameObject.SetActive(false);
        defendCircle.gameObject.SetActive(false);
    }

    public void SetStateOfCircle(StateOfCircle stateOf)
    {
        switch (stateOf)
        {
            case StateOfCircle.attackCircle:
                attackCircle.gameObject.SetActive(true);
                defendCircle.gameObject.SetActive(false);
                break;
            case StateOfCircle.defenseCircle:
                attackCircle.gameObject.SetActive(false);
                defendCircle.gameObject.SetActive(true);
                break;
        }
      //  stateOfCircle = stateOf;
    }

    private void SetSpeedVFX(int countOfSpeedBuffs)
    {
        switch (countOfSpeedBuffs)
        {
            case 1: 
                vFXTotalSpawner.AddPermanentEffect("Speed_1", "SpeedVFX", gameObject);
                break;
            case 2:
                vFXTotalSpawner.AddPermanentEffect("Speed_2", "SpeedVFX", gameObject);
                break;
            case 3:
                vFXTotalSpawner.AddPermanentEffect("Speed_3", "SpeedVFX", gameObject);
                break;
            default:
                vFXTotalSpawner.DestroyPermanentEffect("SpeedVFX", gameObject);
                break;
        }
    }

    private void OnDisable()
    {
        OnDisableTank?.Invoke();
        playersStats.OnHealthChange -= ShowExtraLifeCircle;
    }
}
