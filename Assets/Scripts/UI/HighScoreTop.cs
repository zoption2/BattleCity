using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class HighScoreTop : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformsList;
    private HighScores highScores;
    private HighScoreEntry thisLvlHiScore;
    private HighScoreEntry[] thisLvlScores;

    private TotalSpawner totalSpawner;

    private string streamingAssetPath  { get { return Application.streamingAssetsPath + "/"; }}

    private void SaveHighScoreData(string fileName, string data)
    {
        var path = Path.Combine(streamingAssetPath, fileName);
        File.WriteAllText(path, data);
    }
    public string LoadHighScoreData (string fileName)
    {
        var path = Path.Combine(streamingAssetPath, fileName);
        var data = File.ReadAllText(path);
        return data;
    }

    private void Awake()
    {
        ////отключить глобал пул, чтоб не было видно лишних обьектов в меню
        //totalSpawner = GameObject.FindObjectOfType<TotalSpawner>();
        //totalSpawner.gameObject.SetActive(false);

        entryContainer = transform.Find("HighScoreConteiner");
        entryTemplate = entryContainer.Find("HighScore");

        entryTemplate.gameObject.SetActive(false);


        string jsonString = LoadHighScoreData("Save.txt");
        highScores = JsonUtility.FromJson<HighScores>(jsonString);

        if (highScores == null)
        {
            HighScoreCalculation();
            jsonString = LoadHighScoreData("Save.txt");
            highScores = JsonUtility.FromJson<HighScores>(jsonString);
        }
        else HighScoreCalculation();

        // sorting scores by top
        for (int i = 0; i < highScores.highScoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highScores.highScoreEntryList.Count; j++)
            {
                if (highScores.highScoreEntryList[j].score > highScores.highScoreEntryList[i].score)
                {
                    //swap
                    HighScoreEntry tmp = highScores.highScoreEntryList[i];
                    highScores.highScoreEntryList[i] = highScores.highScoreEntryList[j];
                    highScores.highScoreEntryList[j] = tmp;
                }
            }
        }

        //save TOP player high score
        HighScoreEntry topHiScore = highScores.highScoreEntryList[0];
        string HiScore = JsonUtility.ToJson(topHiScore);
        SaveHighScoreData("TopPlayer.txt", HiScore);


        //delete all indexes lager then 10
        for (int i = highScores.highScoreEntryList.Count - 1; i > 9; i--)
        {
            var needToDel = highScores.highScoreEntryList[i];
            highScores.highScoreEntryList.Remove(needToDel);
        }

        highscoreEntryTransformsList = new List<Transform>();
        foreach (HighScoreEntry highScoreEntry in highScores.highScoreEntryList)
        {
            CreateHighScoreEntryTransform(highScoreEntry, entryContainer, highscoreEntryTransformsList);
        }

        // save new HiScore to file
        string json = JsonUtility.ToJson(highScores);
        SaveHighScoreData("Save.txt", json);

    } 

    private void CreateHighScoreEntryTransform(HighScoreEntry highScoreEntry, Transform conteiner, List<Transform> transformsList)
    {
        float templateHigh = 60f;
        Transform entryTransform = Instantiate(entryTemplate, conteiner);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHigh * transformsList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformsList.Count + 1;


        entryTransform.Find("Pos").GetComponent<Text>().text = rank.ToString();

        string player = highScoreEntry.name;
        entryTransform.Find("Player").GetComponent<Text>().text = player;

        int score = highScoreEntry.score;
        entryTransform.Find("Score").GetComponent<Text>().text = score.ToString();

        string titleCode = highScoreEntry.rank;
        string title = Rank.GetRankByCode(titleCode);
        entryTransform.Find("Title").GetComponent<Text>().text = title;

        transformsList.Add(entryTransform);

        // Set background visible odds and evens, easier to read
        entryTransform.Find("back").gameObject.SetActive(rank % 2 == 1);

        // Highlight First
        //if (rank == 1)
        //{
        //    entryTransform.Find("Pos").GetComponent<Text>().color = Color.green;
        //    entryTransform.Find("Score").GetComponent<Text>().color = Color.green;
        //    entryTransform.Find("Player").GetComponent<Text>().color = Color.green;
        //}

        Transform star = entryTransform.Find("Star");
        switch (rank)
        {
            default:
                star.gameObject.SetActive(false);
                break;
            case 1:
                star.GetComponent<Image>().color = new Color(1, 0.82f, 0);
                break;
            case 2:
                star.GetComponent<Image>().color = new Color(0.76f, 0.76f, 0.76f);
                break;
            case 3:
                star.GetComponent<Image>().color = new Color(0.53f, 0.27f, 0.09f);
                break;

        }

        // ТЕСТ. Подсветка рекорда из конкретно этого раунда. 
        foreach (var item in thisLvlScores)
        {
            if (highScoreEntry == item)
            {
                var back = entryTransform.Find("back");
                back.gameObject.SetActive(true);
                back.GetComponent<Image>().color = new Color(0.68f, 0.33f, 0.07f);
                entryTransform.Find("Pos").GetComponent<Text>().color = Color.white;
                entryTransform.Find("Score").GetComponent<Text>().color = Color.green;
                entryTransform.Find("Player").GetComponent<Text>().color = Color.white;
            }
        }
    }


    private class HighScores
    {
        public List<HighScoreEntry> highScoreEntryList;
    }

    //private void HighScoreCalculation()
    //{
    //    string championName = WhoIsChampion();
    //    int championScore = MasterController.playerScores[championName];
    //    string championRank = Rank.RankOn(championName);

    //    //HighScoreEntry highScoreEntry = new HighScoreEntry { name = Conteiner.tankName[championName], score = championScore, rank = championRank };
    //    thisLvlHiScore = new HighScoreEntry { name = Conteiner.tankName[championName], score = championScore, rank = championRank };

    //    string jsonString = LoadHighScoreData("Save.txt");

    //    highScores = JsonUtility.FromJson<HighScores>(jsonString);

    //    if (highScores == null)
    //    {
    //        highScores = new HighScores() {highScoreEntryList = new List<HighScoreEntry>() };
    //    }

    //    // add new entry to HighScores
    //    //highScores.highScoreEntryList.Add(highScoreEntry);
    //    highScores.highScoreEntryList.Add(thisLvlHiScore);

    //    // save TOP HiScore to file
    //    string json = JsonUtility.ToJson(highScores);
    //    //PlayerPrefs.SetString("HighScore", json);
    //    //PlayerPrefs.Save();
    //    SaveHighScoreData("Save.txt", json);
    //}

    private void HighScoreCalculation()
    {
        thisLvlScores = new HighScoreEntry[4];
        int playersInThisSession = MasterController.totalPlayersInGame;

        for (int i = 0; i < playersInThisSession; i++)
        {
            string player = "Player_" + (i + 1).ToString();
            string playerName = Conteiner.tankName[player];
            int playerScore = MasterController.playerScores[player];
            //string playerRank = Rank.RankOn(player);
            string playerRank = Rank.GetRankCode(player);
            thisLvlScores[i] = new HighScoreEntry { name = playerName, score = playerScore, rank = playerRank };
        }

        string jsonString = LoadHighScoreData("Save.txt");

        highScores = JsonUtility.FromJson<HighScores>(jsonString);

        if (highScores == null)
        {
            highScores = new HighScores() { highScoreEntryList = new List<HighScoreEntry>() };
        }

        // add new entry to HighScores
        //highScores.highScoreEntryList.Add(highScoreEntry);

        foreach (var item in thisLvlScores)
        {
            highScores.highScoreEntryList.Add(item);
        }

        // save TOP HiScore to file
        string json = JsonUtility.ToJson(highScores);
        //PlayerPrefs.SetString("HighScore", json);
        //PlayerPrefs.Save();
        SaveHighScoreData("Save.txt", json);
    }

    //private string WhoIsChampion()
    //{
    //    int first = MasterController.playerScores["Player_1"];
    //    int second = MasterController.playerScores["Player_2"];
    //    int therd = MasterController.playerScores["Player_3"];
    //    int four = MasterController.playerScores["Player_4"];

    //    int champ = Mathf.Max(first, second, therd, four);

    //    if (champ == first) return "Player_1";
    //    else if (champ == second) return "Player_2";
    //    else if (champ == therd) return "Player_3";
    //    else if (champ == four) return "Player_4";
    //    else return null;
    //}
}

[System.Serializable]
public class HighScoreEntry
{
    public int score;
    public string name;
    public string rank;
}








