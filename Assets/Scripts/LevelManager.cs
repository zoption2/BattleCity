using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public UnityAction OnTotalEnemiesChange;
    public UnityAction OnEnemiesOnScreenChange;

    public static int smallEnemy, fastEnemy, bigEnemy, armoredEnemy, bossEnemy;
    private static int currentOnTheScreen; //сколько врагов сейчас на игровом поле
    public static int needToBeOnTheScreen; // сколько должно быть врагов на игровом поле
    public static float spawnRate { get; private set; } // время респаума

    public static float buffSpawnRate; // вероятность появления 
    [SerializeField] private int smallEnemiesInThisLevel, fastEnemiesInThisLevel, bigEnemiesInThisLevel, armoredEnemiesInThisLevel, bossEnemiesInThisLevel, stageNumberOfThisLevel;
    [SerializeField] private float spawnRateInThisLevel = 1f;
    [SerializeField] private int countOfEnemiesAtScreen = 4;
    [SerializeField] private float buffDealerSpawnRate = 10f;

    private static int totalCountOfEnemies; // общее количество врагов на этом уровне

    private List<GameObject> activeEnemies;

    public  List<GameObject> GetAllEnemies()
    {
        return activeEnemies;
    }
    private void SetLevelConfigs()
    {
        var lvl = MasterController.stageNumber;
        Config config = LevelConfig.Instance.Configs(lvl);

        smallEnemy = config.smallEnemies;
        fastEnemy = config.fastEnemies;
        bigEnemy = config.bigEnemies;
        armoredEnemy = config.bigEnemies;
        spawnRate = config.spawnRate;
        needToBeOnTheScreen = config.countOfEnemiesAtScreen + countMidifier();
        buffSpawnRate = config.buffDealerSpawnRate;
        totalCountOfEnemies = smallEnemy + fastEnemy + bigEnemy + armoredEnemy + bossEnemy;
    }


    public bool NeedNewResp
    {
        get 
        {
            if (EnemiesAtScreen < needToBeOnTheScreen && TotalEnemies >= needToBeOnTheScreen)
            {
                return true;
            }
            else if (EnemiesAtScreen < TotalEnemies && TotalEnemies < needToBeOnTheScreen)
            {
                return true;
            }
            else return false;
        }
    }

    private void DoTotalEnemiesChange()
    {
        OnTotalEnemiesChange?.Invoke();
    }
    private void DoEnemiesOnScreenChange()
    {
        OnEnemiesOnScreenChange?.Invoke();
    }
    public int TotalEnemies 
    {
        get
        {
            return totalCountOfEnemies;
        }
        set
        {
            totalCountOfEnemies += value;
            DoTotalEnemiesChange();
        }
    }

    public int EnemiesAtScreen
    {
        get
        {
            return currentOnTheScreen;
        }
        set
        {
            currentOnTheScreen += value;
        }
    }


    public void DestroyEnemy(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
        EnemiesAtScreen = -1;
        TotalEnemies = -1;
        //if (NeedNewResp)
        //{
        //    DoEnemiesOnScreenChange();
        //}
    }

    public void SetEnemy (GameObject enemy)
    {
        activeEnemies.Add(enemy);
        //var currentCount = activeEnemies.Count;
        //if (currentOnTheScreen != currentCount)
        //{
        //    currentOnTheScreen = currentCount;
        //    Debug.LogWarning("Different count of enemies at screen. May be troubles.");
        //}

        //if (NeedNewResp)
        //{
        //    DoEnemiesOnScreenChange();
        //}
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        //MasterController.stageNumber = stageNumberOfThisLevel;
        //smallEnemy = smallEnemiesInThisLevel;
        //fastEnemy = fastEnemiesInThisLevel;
        //bigEnemy = bigEnemiesInThisLevel;
        //armoredEnemy = armoredEnemiesInThisLevel;
        //bossEnemy = bossEnemiesInThisLevel;
        //spawnRate = spawnRateInThisLevel;
        //needToBeOnTheScreen = countOfEnemiesAtScreen + countMidifier();
        //buffSpawnRate = buffDealerSpawnRate;
        SetLevelConfigs();
        currentOnTheScreen = 0;
        //TotalEnemies = smallEnemy + fastEnemy + bigEnemy + armoredEnemy + bossEnemy;
        activeEnemies = new List<GameObject>();
    }

    private int countMidifier()
    {
        switch (MasterController.totalPlayersInGame)
        {
            case 1:  return 0;
            case 2:  return 2;
            case 3:  return 3;
            case 4:  return 4;
            default: goto case 1;
        }
    }
}
