using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayersUI : MonoBehaviour
{
    public static PlayersUI instance;

    [SerializeField] private Image background;
    [SerializeField] private GameObject panel1;
    [SerializeField] private GameObject panel2;
    [SerializeField] private GameObject panel3;
    [SerializeField] private GameObject panel4;

    [SerializeField] private Text currentStage;
    [SerializeField] private Text enemiesLeft;


    [System.Serializable]
    private class PlayerInfo
    {
        public Text name;
        public Text rank;
        public Text lives;
        public Image star1;
        public Image star2;
        public Image star3;
        public Image star4;
        public Image star5;
        public Image projectile1, projectile2, projectile3;
        public Image champion;
        public Image type;
        public PlayerInfo(Text name, Text rank, Text lives, Image star1, Image star2, Image star3, Image star4, Image star5, Image projectile1, Image projectile2, Image projectile3, Image champion, Image type)
        {
            this.name = name;
            this.rank = rank;
            this.lives = lives;
            this.star1 = star1;
            this.star2 = star2;
            this.star3 = star3;
            this.star4 = star4;
            this.star5 = star5;
            this.projectile1 = projectile1;
            this.projectile2 = projectile2;
            this.projectile3 = projectile3;
            this.champion = champion;
            this.type = type;
        }
    }

    [SerializeField] private PlayerInfo info1;
    [SerializeField] private PlayerInfo info2;
    [SerializeField] private PlayerInfo info3;
    [SerializeField] private PlayerInfo info4;

    private List<PlayerInfo> ActiveInfos;
    private LevelManager levelManager;
    private int players;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        players = MasterController.totalPlayersInGame;
        levelManager = LevelManager.Instance;
        currentStage.text = MasterController.stageNumber.ToString();
        UpdateTotalEnemiesCount();
        ActivePlayers();
        if (players > 0) UpdateInfo("Player_1", info1);
        if (players > 1) UpdateInfo("Player_2", info2);
        if (players > 2) UpdateInfo("Player_3", info3);
        if (players > 3) UpdateInfo("Player_4", info4);
        background.rectTransform.Translate(new Vector3(0, 0, 0));

        levelManager.OnTotalEnemiesChange += UpdateTotalEnemiesCount;
    }

    private void ActivePlayers()
    {
        var playersCount = MasterController.totalPlayersInGame;

        panel1.SetActive(playersCount >= 1);
        panel2.SetActive(playersCount >= 2);
        panel3.SetActive(playersCount >= 3);
        panel4.SetActive(playersCount >= 4);
    }
    private void UpdateInfo(string playerTag, PlayerInfo playerNumber)
    {
        playerNumber.name.text = Conteiner.tankName[playerTag].ToString();
        playerNumber.rank.text = Rank.RankOn(playerTag);
        UpdateLifeInfo(playerTag, MasterController.playerLives[playerTag]);
        UpdateProjectileInfo(playerTag, MasterController.playerBoosters[playerTag]);
        UpdateStarInfo(playerTag, MasterController.playerStars[playerTag]);
        playerNumber.type.sprite = GetTypeImage(playerTag);
        if (playerTag == MasterController.NameOfChampion) playerNumber.champion.enabled = true;
        else playerNumber.champion.enabled = false;

    }

    private Sprite GetTypeImage(string playerName)
    {
        var player = Conteiner.tankType[playerName];
        switch (player)
        {
            case 1: return Resources.Load("Textures/UI/Attacker", typeof(Sprite)) as Sprite;
            case 2: return Resources.Load("Textures/UI/Defender", typeof(Sprite)) as Sprite;
            case 3: return Resources.Load("Textures/UI/Buffer", typeof(Sprite)) as Sprite;
            case 4: return Resources.Load("Textures/UI/Melee", typeof(Sprite)) as Sprite;
            case 5: return Resources.Load("Textures/UI/Esquire", typeof(Sprite)) as Sprite;

            default: return null;
        }
    }
    public void RevealUI()
    {
        StartCoroutine(RevealUIRoutin());
    }
    private IEnumerator RevealUIRoutin()
    {
        yield return new WaitForSeconds(3.5f);
        float moveToDisplay = background.rectTransform.position.x -435;
        while (background.rectTransform.position.x > moveToDisplay)
        {
            background.rectTransform.Translate(new Vector3(-300 * Time.deltaTime, 0, 0));
            yield return null;
        }
    }

    private PlayerInfo playerInfo(string player)
    {
        switch (player)
        {
            case "Player_1": return info1;
            case "Player_2": return info2;
            case "Player_3": return info3;
            case "Player_4": return info4;
            default: return info1;
        }
    }

    public void UpdateTotalEnemiesCount()
    {
        enemiesLeft.text = levelManager.TotalEnemies.ToString();
    }
    public void UpdateLifeInfo(string player, int value)
    {
        PlayerInfo info = playerInfo(player);
        info.lives.text = value.ToString();
    }

    public void UpdateStarInfo(string player, int value)
    {
        PlayerInfo playerNumber = playerInfo(player);

        playerNumber.star1.enabled = (value >= 1);
        playerNumber.star1.color = Color.white;
        playerNumber.star2.enabled = (value >= 2);
        playerNumber.star2.color = Color.white;
        playerNumber.star3.enabled = (value >= 3);
        playerNumber.star3.color = Color.white;
        playerNumber.star4.enabled = (value >= 4);
        playerNumber.star4.color = Color.white;
        playerNumber.star5.enabled = (value >= 5);
        playerNumber.star5.color = Color.white;

    }
    public void UpdateProjectileInfo(string player, int value)
    {
        PlayerInfo playerNumber = playerInfo(player);

        playerNumber.projectile1.enabled = (value >= 1);
        playerNumber.projectile2.enabled = (value >= 2);
        playerNumber.projectile3.enabled = (value >= 3);
    }

    public void BuffStars(string player, int value)
    {
        PlayerInfo playerNumber = playerInfo(player);
        List<Image> buffStars = new List<Image>();

        UpdateStarInfo(player, value);

        if (playerNumber.star1.IsActive()) buffStars.Add(playerNumber.star1);
        if (playerNumber.star2.IsActive()) buffStars.Add(playerNumber.star2);
        if (playerNumber.star3.IsActive()) buffStars.Add(playerNumber.star3);
        if (playerNumber.star4.IsActive()) buffStars.Add(playerNumber.star4);
        if (playerNumber.star5.IsActive()) buffStars.Add(playerNumber.star5);

        buffStars[buffStars.Count - 1].color = Color.red;
        buffStars[buffStars.Count - 2].color = Color.red;
    }
        
}
