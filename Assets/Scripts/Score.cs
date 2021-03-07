using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class Score : MonoBehaviour
{
    [SerializeField] private Text hiScoreText;
    [SerializeField] private Text stageText;
    private SoundManager soundManager;
   
    [System.Serializable]
    public class PlayerScore
    {
        public Text playerName;
        public Text playerScoreText;
        public Text rank;
        public Text smallTanksDestroyed;
        public Text fastTanksDestroyed;
        public Text bigTanksDestroyed;
        public Text armoredTanksDestroyed;
        public Text totalTanksDestroyed;
        public Text bonusScore;

        [HideInInspector] public int smallTankScore;
        [HideInInspector] public int fastTankScore;
        [HideInInspector] public int bigTankScore;
        [HideInInspector] public int armoredTankScore;

        public PlayerScore(
                  Text playerName,
                  Text playerScoreText,
                  Text smallTanksDestroyed,
                  Text fastTanksDestroyed,
                  Text bigTanksDestroyed,
                  Text armoredTanksDestroyed,
                  Text totalTanksDestroyed,
                  Text bonusScore,
                  Text rank)
        {

        this.playerName = playerName;
        this.rank = rank;
        this.playerScoreText = playerScoreText;
        this.smallTanksDestroyed = smallTanksDestroyed;
        this.fastTanksDestroyed = fastTanksDestroyed;
        this.bigTanksDestroyed = bigTanksDestroyed;
        this.armoredTanksDestroyed = armoredTanksDestroyed;
        this.totalTanksDestroyed = totalTanksDestroyed;
        this.bonusScore = bonusScore;
    }
    }


    [SerializeField] private PlayerScore player1Score;
    [SerializeField] private PlayerScore player2Score;
    [SerializeField] private PlayerScore player3Score;
    [SerializeField] private PlayerScore player4Score;



    private int smallTankScore;
    private int fastTankScore;
    private int bigTankScore;
    private int armoredTankScore;
    private int smallTankPointsWorth;
    private int fastTankPointsWorth;
    private int bigTankPointsWorth;
    private int armoredTankPointsWorth;

    private Dictionary<string, bool> _currentStatEnd;


    private AsyncOperation loadingOperation;

    private void Start()
    {
        smallTankPointsWorth = MasterController.smallEnemyPoints;
        fastTankPointsWorth = MasterController.fastEnemyPoints;
        bigTankPointsWorth = MasterController.bigEnemyPoints;
        armoredTankPointsWorth = MasterController.armoredEnemyPoints;
        stageText.text = MasterController.stageNumber.ToString();
        player1Score.playerScoreText.text = MasterController.playerScores["Player_1"].ToString();
        player2Score.playerScoreText.text = MasterController.playerScores["Player_2"].ToString();
        player3Score.playerScoreText.text = MasterController.playerScores["Player_3"].ToString();
        player4Score.playerScoreText.text = MasterController.playerScores["Player_4"].ToString();
        player1Score.rank.text = Rank.RankOn("Player_1");
        player2Score.rank.text = Rank.RankOn("Player_2");
        player3Score.rank.text = Rank.RankOn("Player_3");
        player4Score.rank.text = Rank.RankOn("Player_4");
        soundManager = SoundManager.play;

        soundManager.StopPlaySound("Any");
        if (MasterController.stageClear == false)
        {
            soundManager.PlayContinuousAudio("Astronomia", 0.2f);
        }

        _currentStatEnd = new Dictionary<string, bool>();

        switch (MasterController.totalPlayersInGame)
        {
            case 1:  
                StartCoroutine(UpdateTankPoints("Player_1", player1Score));
                break;
            case 2:
                StartCoroutine(UpdateTankPoints("Player_1", player1Score));
                StartCoroutine(UpdateTankPoints("Player_2", player2Score));
                break;
            case 3: 
                StartCoroutine(UpdateTankPoints("Player_1", player1Score));
                StartCoroutine(UpdateTankPoints("Player_2", player2Score));
                StartCoroutine(UpdateTankPoints("Player_3", player3Score));
                break;
            case 4:  
                StartCoroutine(UpdateTankPoints("Player_1", player1Score));
                StartCoroutine(UpdateTankPoints("Player_2", player2Score));
                StartCoroutine(UpdateTankPoints("Player_3", player3Score));
                StartCoroutine(UpdateTankPoints("Player_4", player4Score));
                break;
                
            default:
                break;
        }

        //load TOP player high score;
        var path = Application.streamingAssetsPath + "/TopPlayer.txt";
        string json = File.ReadAllText(path);
        if (json != null)
        {
            HighScoreEntry topHiScore = JsonUtility.FromJson<HighScoreEntry>(json);

            var rankTranslate = Rank.GetRankByCode(topHiScore.rank);
            hiScoreText.text = rankTranslate + " " + topHiScore.name + " - " + topHiScore.score;
        }
        
    }

    private IEnumerator UpdateTankPoints(string playerTag, PlayerScore playerN)
    {
        _currentStatEnd.Add(playerTag, false);

        playerN.playerName.text = Conteiner.tankName[playerTag].ToString();

        for (int i = 0; i <= MasterController.smallTanks[playerTag]; i++)
        {
            playerN.smallTankScore = smallTankPointsWorth * i;
            playerN.smallTanksDestroyed.text = i.ToString();
            yield return new WaitForSeconds(0.1f);
        }

        for (int i = 0; i <= MasterController.fastTanks[playerTag]; i++)
        {
            playerN.fastTankScore = fastTankPointsWorth * i;
            playerN.fastTanksDestroyed.text = i.ToString();
            yield return new WaitForSeconds(0.1f);
        }

        for (int i = 0; i <= MasterController.bigTanks[playerTag]; i++)
        {
            playerN.bigTankScore = bigTankPointsWorth * i;
            playerN.bigTanksDestroyed.text = i.ToString();
            yield return new WaitForSeconds(0.1f);
        }

        for (int i = 0; i <= MasterController.armoredTanks[playerTag]; i++)
        {
            playerN.armoredTankScore = armoredTankPointsWorth * i;
            playerN.armoredTanksDestroyed.text = i.ToString();
            yield return new WaitForSeconds(0.1f);
        }

        playerN.totalTanksDestroyed.text = (MasterController.smallTanks[playerTag] + MasterController.fastTanks[playerTag] + MasterController.bigTanks[playerTag] + MasterController.armoredTanks[playerTag]).ToString();
        MasterController.playerScores[playerTag] += (playerN.smallTankScore + playerN.fastTankScore + playerN.bigTankScore + playerN.armoredTankScore + MasterController.bonusScores[playerTag]);

        yield return new WaitForSeconds(0.5f);
        playerN.bonusScore.text = MasterController.bonusScores[playerTag].ToString();
        yield return new WaitForSeconds(0.5f);
        playerN.playerScoreText.text = MasterController.playerScores[playerTag].ToString();
        yield return new WaitForSeconds(0.5f);
        playerN.rank.text = Rank.RankOn(playerTag);

        _currentStatEnd[playerTag] = true;

        EndStatistic();
    }

    private void EndStatistic()
    {
        foreach (var item in _currentStatEnd)
        {
            if (item.Value == false) return;
        }
        StartCoroutine(endStatistic());
    }
    private IEnumerator endStatistic()
    {
        switch (MasterController.difficult)
        {
            case Difficult.normal:
                loadingOperation = SceneManager.LoadSceneAsync("Constructor");
                break;
            case Difficult.hard:
                loadingOperation = SceneManager.LoadSceneAsync("Hardcore");
                break;
        }

        loadingOperation.allowSceneActivation = false;
        //soundManager.StopPlaySound("Any");
        MasterController.NameOfChampion = WhoIsChampion();

        yield return new WaitForSeconds(5f);
        if (MasterController.stageClear)
        {
            ClearStatistics();
            loadingOperation.allowSceneActivation = true;
        }
        else
        {
            ClearStatistics();
            //load gameover scene
            SceneManager.LoadScene("GameOver");
        }
    }
 
    private void ClearStatistics()
    {
        MasterController.EnemyCounterToNull();
        MasterController.stageClear = false;
        MasterController.stageNumber++;
    }

    private string WhoIsChampion()
    {
        int first = MasterController.playerScores["Player_1"];
        int second = MasterController.playerScores["Player_2"];
        int therd = MasterController.playerScores["Player_3"];
        int four = MasterController.playerScores["Player_4"];
        
        int champ = Mathf.Max(first, second, therd, four);

        if (champ == first) return "Player_1";
        else if (champ == second) return "Player_2";
        else if (champ == therd) return "Player_3";
        else if (champ == four) return "Player_4";
        else return null;
    }


}

      
