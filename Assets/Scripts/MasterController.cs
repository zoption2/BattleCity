using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficult { normal, hard }
public class MasterController : MonoBehaviour, I_DontDestroy
{
    [HideInInspector] public static int smallEnemyPoints = 100, fastEnemyPoints = 200, bigEnemyPoints = 300, armoredEnemyPoints = 400;
    public int smallEnemyPointsWorth { get { return smallEnemyPoints; } }
    public int fastEnemyPointsWorth { get { return fastEnemyPoints; } }
    public int bigEnemyPointsWorth { get { return bigEnemyPoints; } }
    public int armoredEnemyPointsWorth { get { return armoredEnemyPoints; } }

    //public static MasterController instance;


    public const int MAX_STARS = 5;
    public const int MAX_PROJECTILES = 3;
    public const float MAX_HEALTH = 2;

    public static int totalPlayersInGame = 1;
    public static string NameOfChampion;

    public static int smallEnemyDestroyed, fastEnemyDestroyed, bigEnemyDestroyed, armoredEnemyDestroyed;
    public static int stageNumber = 1;

    public static bool stageClear = false;

    public static Dictionary<string, int> smallTanks = new Dictionary<string, int>();    //игрок / количество
    public static Dictionary<string, int> fastTanks = new Dictionary<string, int>();
    public static Dictionary<string, int> bigTanks = new Dictionary<string, int>();
    public static Dictionary<string, int> armoredTanks = new Dictionary<string, int>();

    public static Dictionary<string, int> playerScores = new Dictionary<string, int>();
    public static Dictionary<string, int> playerLives = new Dictionary<string, int>();
    public static Dictionary<string, int> playerStars = new Dictionary<string, int>();
    public static Dictionary<string, int> playerBoosters = new Dictionary<string, int>();
    public static Dictionary<string, int> playersController = new Dictionary<string, int>();
    public static Dictionary<string, int> bonusScores = new Dictionary<string, int>();
    public static Dictionary<string, float> playerSpeed = new Dictionary<string, float>();
    public static Dictionary<string, int> speedBoosts = new Dictionary<string, int>();
    public static Dictionary<string, float> playerHealth = new Dictionary<string, float>();

    public static Dictionary<int, string> whoIsShooter = new Dictionary<int, string>();

    public static Dictionary<string, bool> buffAnchorActive = new Dictionary<string, bool>();
    public static Dictionary<string, bool> buffAxeActive = new Dictionary<string, bool>();
    public static Dictionary<string, bool> isPlayerAlive = new Dictionary<string, bool>();
    public static Dictionary<string, GameObject> activePlayers = new Dictionary<string, GameObject>();
    public static Dictionary<string, bool> playersUnderStarBuff = new Dictionary<string, bool>();

    public static Dictionary<string, bool> isPlayerBoosterUI = new Dictionary<string, bool>();

    public static List<Dictionary<string, int>> dictionariesOfEnemies = new List<Dictionary<string, int>>();
    public static List<string> buffs = new List<string>();


    public static Difficult difficult;



    //#region Singleton
    //private void OnEnable()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(this.gameObject);
    //    }
    //    else if (instance != this)
    //    {
    //        Destroy(this);
    //    }
    //}

    //#endregion

    public static void StartNewGame()
    {
        CreateNew();
        //GlobalWatcher.instance.AddDontDestroeble(this);
        LoadListOfDictionaries();
        PrepareForGame();
        PlayerScoreStart();
        PlayerLivesStart();
        PlayerStarsStart();
        PlayerProjectilesStart();
        CanSail();
        BuffsAdd();
        AddAliveStatus();
        AddPlayerBoosterUI();
        AddSpeed();
        AddSpeedBoosts();
        AddHealth();
        AddUnderStarBuffPlayers();
    }

    private static void CreateNew()
    {
        smallTanks = new Dictionary<string, int>();    //игрок / количество
        fastTanks = new Dictionary<string, int>();
        bigTanks = new Dictionary<string, int>();
        armoredTanks = new Dictionary<string, int>();
        playerScores = new Dictionary<string, int>();
        playerLives = new Dictionary<string, int>();
        playerStars = new Dictionary<string, int>();
        playerBoosters = new Dictionary<string, int>();
        playersController = new Dictionary<string, int>();
        bonusScores = new Dictionary<string, int>();
        whoIsShooter = new Dictionary<int, string>();
        buffAnchorActive = new Dictionary<string, bool>();
        buffAxeActive = new Dictionary<string, bool>();
        isPlayerAlive = new Dictionary<string, bool>();
        activePlayers = new Dictionary<string, GameObject>();
        isPlayerBoosterUI = new Dictionary<string, bool>();
        dictionariesOfEnemies = new List<Dictionary<string, int>>();
        playerSpeed = new Dictionary<string, float>();
        speedBoosts = new Dictionary<string, int>();
        playersUnderStarBuff = new Dictionary<string, bool>();
        buffs = new List<string>();
        playerHealth = new Dictionary<string, float>();
        stageNumber = 1;
    }

    public static void AddUnderStarBuffPlayers()
    {
        playersUnderStarBuff.Add("Player_1", false);
        playersUnderStarBuff.Add("Player_2", false);
        playersUnderStarBuff.Add("Player_3", false);
        playersUnderStarBuff.Add("Player_4", false);
    }
    public static void AddHealth()
    {
        playerHealth.Add("Player_1", 1);
        playerHealth.Add("Player_2", 1);
        playerHealth.Add("Player_3", 1);
        playerHealth.Add("Player_4", 1);
    }
    public static void AddSpeedBoosts()
    {
        speedBoosts.Add("Player_1", 0);
        speedBoosts.Add("Player_2", 0);
        speedBoosts.Add("Player_3", 0);
        speedBoosts.Add("Player_4", 0);
    }
    public static void AddSpeed()
    {
        playerSpeed.Add("Player_1", 5);
        playerSpeed.Add("Player_2", 5);
        playerSpeed.Add("Player_3", 5);
        playerSpeed.Add("Player_4", 5);
    }

    public static void EnemyCounterToNull()
    {
        foreach (var item in dictionariesOfEnemies)
        {
            item["Player_1"] = 0;
            item["Player_2"] = 0;
            item["Player_3"] = 0;
            item["Player_4"] = 0;
        }
    }

    private static void PrepareForGame()
    {
        foreach (var item in dictionariesOfEnemies)
        {
            item.Clear();
            item.Add("Player_1", 0);
            item.Add("Player_2", 0);
            item.Add("Player_3", 0);
            item.Add("Player_4", 0);
        }
    }

    private static void BuffsAdd()
    {
        buffs.Add("StarBuff");
        buffs.Add("StarBuff");
        buffs.Add("HeartBuff");
        buffs.Add("DiggerBuff");
        buffs.Add("GunBuff");
        buffs.Add("TimerBuff");
        buffs.Add("AnchorBuff");
        buffs.Add("GranadeBuff");
        buffs.Add("HelmetBuff");
        buffs.Add("AxeBuff");
        buffs.Add("SpeedBuff");
        buffs.Add("ExtraLifeBuff");
    }

    private static void LoadListOfDictionaries()
    {
        dictionariesOfEnemies.Add(smallTanks);
        dictionariesOfEnemies.Add(fastTanks);
        dictionariesOfEnemies.Add(bigTanks);
        dictionariesOfEnemies.Add(armoredTanks);
        dictionariesOfEnemies.Add(bonusScores);
    }
    
    private static void PlayerScoreStart()
    {
        playerScores.Add("Player_1", 0);
        playerScores.Add("Player_2", 0);
        playerScores.Add("Player_3", 0);
        playerScores.Add("Player_4", 0);
    }

    private static void PlayerLivesStart()
    {
        playerLives.Add("Player_1", 3);
        playerLives.Add("Player_2", 3);
        playerLives.Add("Player_3", 3);
        playerLives.Add("Player_4", 3);
    }

    private static void PlayerStarsStart()
    {
        playerStars.Add("Player_1", 0);
        playerStars.Add("Player_2", 0);
        playerStars.Add("Player_3", 0);
        playerStars.Add("Player_4", 0);
    }

    private static void PlayerProjectilesStart()
    {
        playerBoosters.Add("Player_1", 0);
        playerBoosters.Add("Player_2", 0);
        playerBoosters.Add("Player_3", 0);
        playerBoosters.Add("Player_4", 0);
    }

    private static void CanSail()
    {
        buffAnchorActive.Add("Player_1", false);
        buffAnchorActive.Add("Player_2", false);
        buffAnchorActive.Add("Player_3", false);
        buffAnchorActive.Add("Player_4", false);
    }

    private static void AddAliveStatus()
    {
        isPlayerAlive.Add("Player_1", false);
        isPlayerAlive.Add("Player_2", false);
        isPlayerAlive.Add("Player_3", false);
        isPlayerAlive.Add("Player_4", false);
    }

    private static void AddPlayerBoosterUI()
    {
        isPlayerBoosterUI.Add("Player_1", true);
        isPlayerBoosterUI.Add("Player_2", true);
        isPlayerBoosterUI.Add("Player_3", true);
        isPlayerBoosterUI.Add("Player_4", true);
    }

    public GameObject ReturnGameObject()
    {
        return this.gameObject;
    }
}

