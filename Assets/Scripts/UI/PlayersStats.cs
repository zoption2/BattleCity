using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayersStats : MonoBehaviour
{
    public static PlayersStats Instance;

    public UnityAction<string> OnLivesChanged;
    public UnityAction<string> OnStarsChanged;
    public UnityAction<string> OnProjectilesChanged;
    public UnityAction<string> OnSpeedChanged;
    public UnityAction<string> OnHealthChange;

    public UnityAction<string> OnBuffStars;
    public UnityAction <string> OnTargetProjectilesUp;

    private  Dictionary<string, bool> starUpBoostActive = new Dictionary<string, bool>();
    private const float SPEED_BOOST_COUNT = 0.7f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(this);
    }

    public void DoTargetProjectilesUp(string name)
    {
        OnTargetProjectilesUp?.Invoke(name);
    }

    public void DoBuffStars(string name)
    {
        OnBuffStars?.Invoke(name);
    }

    public void DoHealthChange(string name)
    {
        OnHealthChange?.Invoke(name);
    }
    public void DoSpeedChange(string name)
    {
        OnSpeedChanged?.Invoke(name);
    }
    public void DoLivesChanged(string name)
    {
        OnLivesChanged?.Invoke(name);
    }
    public void DoStarsChanged(string name)
    {
        OnStarsChanged?.Invoke(name);
    }
    public void DoProjectilesChanged(string name)
    {
        OnProjectilesChanged?.Invoke(name);
    }

    public void SetSpeed(string player, float speed)
    {
        DoSpeedChange(player);
    }


    public float GetHealth(string player)
    {
        return MasterController.playerHealth[player];
    }
    /// <summary>
    /// Здоровье танка игрока. Значение больше 1 даёт возможность получиться несколько попаданий, прежде чем умереть.  
    /// </summary>
    /// <param name="player"></param>
    /// <param name="lives"></param>
    public void SetHealth(string player, float lives)
    {
        var current = MasterController.playerHealth[player];
        var update = Mathf.Clamp(current + lives, 0, MasterController.MAX_HEALTH);
        if (current != update)
        {
            //PlayerName = player;
            MasterController.playerHealth[player] = update;
            DoHealthChange(player);
        }
    }
    public void SetLives(string player, int lives)
    {
        var current = MasterController.playerLives[player];
        var update = current + lives;
        if (current != update)
        {
            //PlayerName = player;
            MasterController.playerLives[player] = update;
            DoLivesChanged(player);
        }
    }

    public int GetLives(string player)
    {
        return MasterController.playerLives[player];
    }

    public void SetStars(string player, int stars)
    {
        var current = MasterController.playerStars[player];
        var newone = Mathf.Clamp(stars + current, 0, MasterController.MAX_STARS);
        if (current != newone)
        {
           // PlayerName = player;
            MasterController.playerStars[player] = newone;
            DoStarsChanged(player);
            if (newone > current)
            {
                Transform tank = MasterController.activePlayers[player].transform;
                VFXTotalSpawner.Instance.PlayEffect("StarUP", tank.position, Quaternion.identity, 2f, tank);

                if (newone == 5)
                {
                    SetHealth(player, 1);
                }
            }
        }
    }
    public void BuffStars(string player, int stars)
    {
        var current = MasterController.playerStars[player];
        var newone = Mathf.Clamp(stars + current, 0, MasterController.MAX_STARS);
        if (current != newone)
        {
            //PlayerName = player;
            MasterController.playerStars[player] = newone;
            DoBuffStars(player);

        }
    }
    public int GetStars(string player)
    {
        return MasterController.playerStars[player];
    }

    public void SetProjectiles(string player, int count)
    {
        var current = MasterController.playerBoosters[player];
        var newone = Mathf.Clamp(count + current, 0, MasterController.MAX_PROJECTILES);
        if (current != newone)
        {
            //PlayerName = player;
            MasterController.playerBoosters[player] = newone;
            DoProjectilesChanged(player);

            if(newone > current)
            {
                DoTargetProjectilesUp(player);
            }
        }
    }
    public int GetProjectiles(string player)
    {
        return MasterController.playerBoosters[player];
    }
}
