using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private Image topCurtain, bottomCurtain, blackCurtain;
    [SerializeField] private Text stageNumber, stageText, gameOverText, levelCompleateText;
    [SerializeField] private RectTransform canvas;
    [SerializeField] private GameObject baseSpawnObjects;
    [SerializeField] private GameObject[] enemySpawnPoints;
    [SerializeField] private GameObject player_1_SpawnPoint;
    [SerializeField] private GameObject player_2_SpawnPoint;
    [SerializeField] private GameObject player_3_SpawnPoint;
    [SerializeField] private GameObject player_4_SpawnPoint;
    private static bool startStage;
    private bool firstSpawn;
   // private bool enemyReserveEmpty;
    private static bool needSpawnerWork;
    private bool levelCompleate;
    private int atScreenInThisLvl;
    private PlayersUI playersUI;
    private SoundManager _soundManager;
    private PlayersStats playersStats;
    private LevelManager levelManager;

    public UnityAction OnLevelCompleated;
    public UnityAction OnLevelFailed;


    private void Start()
    {
        //enemySpawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawnPoint");
        //playersUI = FindObjectOfType<PlayersUI>();
        playersUI = PlayersUI.instance;
        //player_1_SpawnPoint = GameObject.FindGameObjectWithTag("Player_1_SpawnPoint");
        //player_2_SpawnPoint = GameObject.FindGameObjectWithTag("Player_2_SpawnPoint");
        //player_3_SpawnPoint = GameObject.FindGameObjectWithTag("Player_3_SpawnPoint");
        //player_4_SpawnPoint = GameObject.FindGameObjectWithTag("Player_4_SpawnPoint");
        playersStats = PlayersStats.Instance;
        levelManager = LevelManager.Instance;
        startStage = true;
        firstSpawn = true;
        needSpawnerWork = false;
        //enemyReserveEmpty = false;
        levelCompleate = true;
        atScreenInThisLvl = LevelManager.needToBeOnTheScreen;
        stageNumber.text = MasterController.stageNumber.ToString();
        _soundManager = SoundManager.play;
        StartCoroutine(StartStage());
        StartCoroutine(UpdateRoutine());

    }

    private void DoLevelFailed()
    {
        OnLevelFailed?.Invoke();
    }
    private void DoLevelCompleate()
    {
        OnLevelCompleated?.Invoke();
    }

    //private void Spawner()
    //{
    //    if (levelManager.TotalEnemies == 0 && levelManager.EnemiesAtScreen == 0)
    //    {
    //        MasterController.stageClear = true;
    //        if (levelCompleate) StartCoroutine(LevelComplete());
    //    }
    //    else
    //    {
    //        if (levelManager.NeedNewResp)
    //        {
    //            SpawnEnemy();
    //        }
    //    }

    //}


    public IEnumerator UpdateRoutine()
    {
        yield return new WaitForSecondsRealtime(1f);
        if (levelManager.TotalEnemies == 0 && levelManager.EnemiesAtScreen == 0)
        {
            MasterController.stageClear = true;
            //if (levelCompleate) 
                StartCoroutine(LevelComplete());
            //Invoke("LevelCompleted", 5f);
            StopCoroutine(UpdateRoutine());
        }
        //else
        //{
        //    if (!firstSpawn && levelManager.NeedNewResp) StartCoroutine(EnemySpawner());
        //}


        if (!MasterController.stageClear)
        {
            StartCoroutine(UpdateRoutine());
        }
    }

    private void LateUpdate()
    {
        if (levelManager.NeedNewResp && needSpawnerWork)
        {
            SpawnEnemy();
        }
    }

    private IEnumerator StartStage()
    {
        CanvasActivate();
        StartCoroutine(RevealStageNumber());
        StartCoroutine(PlayIntro());
        yield return new WaitForSeconds(3);
        StartCoroutine(RevealTopStage());
        StartCoroutine(RevealBottomStage());
        yield return new WaitForSeconds(1);
        StartCoroutine(CameraIntro());
        StartCoroutine(FirstPlayerSpawn());
        playersUI.RevealUI();
        StartCoroutine(EnemyFirstSpawn());
        StartCoroutine(GameplayMusic());
    }

    private IEnumerator PlayIntro()
    {
        _soundManager.PlayContinuousAudio("GameIntro", 0.1f);
        yield return new WaitForSeconds(8f);
        _soundManager.StopPlaySound("GameIntro");
    }
    private IEnumerator GameplayMusic()
    {
        yield return new WaitForSeconds(4f);
        _soundManager.PlayContinuousAudio(_soundManager.GameplaySound(), 0.2f);
        yield return null;
    }

    private void LevelCompleted()
    {
        //enemyReserveEmpty = false;
        SceneManager.LoadScene("Score");
    }

    private void SpawnEnemy()
    {
        int spawnPointIndex = Random.Range(0, enemySpawnPoints.Length);
        Respawn respawn = enemySpawnPoints[spawnPointIndex].GetComponent<Respawn>();
        if (!respawn.isPointBusy)
        {
            respawn.SpawnEnemy();
            //LevelManager.currentOnTheScreen++;
            levelManager.EnemiesAtScreen = 1;
        }

        else return;
    }

    private void SpawnEnemy(int spawnerNumber)
    {
        Respawn respawn = enemySpawnPoints[spawnerNumber].GetComponent<Respawn>();
        if (!respawn.isPointBusy)
        {
            respawn.SpawnEnemy();
            //LevelManager.currentOnTheScreen++;
            levelManager.EnemiesAtScreen = 1;
        }
        else return;
    }


    //spawn 3 enemies at one time at Game start
    private IEnumerator EnemyFirstSpawn()
    {
        yield return new WaitForSeconds(5f);
        SpawnEnemy(0);
        SpawnEnemy(1);
        SpawnEnemy(2);
        if (MasterController.difficult == Difficult.hard)
        {
            SpawnEnemy(3);
        }

        //yield return new WaitForSeconds(2.1f);
        needSpawnerWork = true;
        firstSpawn = false;
    }

    //spawn all players at one time at Game start
    private IEnumerator FirstPlayerSpawn()
    {
        yield return new WaitForSeconds(5f);
        SpawnPlayer("Player_1");
        SpawnPlayer("Player_2");
        SpawnPlayer("Player_3");
        SpawnPlayer("Player_4");
    }

    //player spawn method
    public void SpawnPlayer(string player)
    {
        int playerNumber;

        switch (player)
        {
            case "Player_1": playerNumber = 1; break;
            case "Player_2": playerNumber = 2; break;
            case "Player_3": playerNumber = 3; break;
            case "Player_4": playerNumber = 4; break;
            default: playerNumber = 1; break;
        }

        if ((MasterController.playerLives[player] > 0 && playerNumber <= MasterController.totalPlayersInGame) || MasterController.playerLives[player] == 0 && MasterController.isPlayerAlive[player] == true)
        {
            MasterController.isPlayerAlive[player] = true;

            if (!startStage)
            {
                //MasterController.playerLives[player]--;
                //MasterController.playerBoosters[player] = 0;
                //MasterController.playerStars[player] = 0;
                playersStats.SetLives(player, -1);
                playersStats.SetProjectiles(player, -3);
                playersStats.SetStars(player, -5);
                playersStats.SetHealth(player, 1);

                MasterController.playerSpeed[player] = 5f;
                MasterController.speedBoosts[player] = 0;
                MasterController.buffAnchorActive[player] = false;
                MasterController.buffAxeActive[player] = false;
            }
            if (MasterController.totalPlayersInGame == playerNumber) startStage = false;

            switch (playerNumber)
            {
                case 1:
                    Respawn respawn = player_1_SpawnPoint.GetComponent<Respawn>();
                    respawn.SpawnPlayer(1);
                    break;
                case 2:
                    Respawn respawn2 = player_2_SpawnPoint.GetComponent<Respawn>();
                    respawn2.SpawnPlayer(2); ;
                    break;
                case 3:
                    Respawn respawn3 = player_3_SpawnPoint.GetComponent<Respawn>();
                    respawn3.SpawnPlayer(3); ;
                    break;
                case 4:
                    Respawn respawn4 = player_4_SpawnPoint.GetComponent<Respawn>();
                    respawn4.SpawnPlayer(4); ;
                    break;
            }
        }
        else
        {
            MasterController.isPlayerAlive[player] = false;
            foreach (var alive in MasterController.isPlayerAlive)
            {
                if (alive.Value) return;
            }
            GameOver();
        }
    }

    
    private IEnumerator RevealStageNumber()
    {
        while (blackCurtain.rectTransform.localScale.y >0 )
        {
            blackCurtain.rectTransform.localScale = new Vector3(blackCurtain.rectTransform.localScale.x, Mathf.Clamp(blackCurtain.rectTransform.localScale.y - Time.deltaTime, 0, 1), blackCurtain.rectTransform.localScale.z);
            yield return null;
        }
    }

    private IEnumerator RevealTopStage()
    {
        float moveTopUpMin = topCurtain.rectTransform.position.y + (canvas.rect.height / 2) + 10;
        stageNumber.enabled = false;
        stageText.enabled = false;
        while (topCurtain.rectTransform.position.y < moveTopUpMin)
        {
            topCurtain.rectTransform.Translate(new Vector3(0, 500 * Time.deltaTime, 0));
            yield return null;
        }
    }

    private IEnumerator RevealBottomStage()
    {
        float moveBottomDownMin = bottomCurtain.rectTransform.position.y - (canvas.rect.height / 2) - 10;
        while (bottomCurtain.rectTransform.position.y > moveBottomDownMin)
        {
            bottomCurtain.rectTransform.Translate(new Vector3(0, -500 * Time.deltaTime, 0));
            yield return null;
        }
    }
    public void GameOver()
    {
        if (levelCompleate) StartCoroutine(GameOverRoutin());
        else Debug.LogError("This level have not compleate yet. Wrong GameOver call!");
    }
    private IEnumerator GameOverRoutin()
    {
        gameOverText.enabled = true;
        DoLevelFailed();
        _soundManager.StopPlaySoundImmediately("Any");
        _soundManager.IsSoundOn = false;
        _soundManager.PlayContinuousAudio("DirectedBy", 0.5f);

        levelCompleate = false;
        while (gameOverText.rectTransform.localPosition.y < 0)
        {
            gameOverText.rectTransform.localPosition = new Vector3(gameOverText.rectTransform.localPosition.x, gameOverText.rectTransform.localPosition.y + 100f * Time.deltaTime, gameOverText.rectTransform.localPosition.z);
            yield return null;
        }
        MasterController.stageClear = false;
        LevelCompleted();
    }

    private IEnumerator LevelComplete()
    {
        levelCompleateText.enabled = true;
        levelCompleate = false;
        DoLevelCompleate();
        while (levelCompleateText.rectTransform.localPosition.y > 0)
        {
            levelCompleateText.rectTransform.Translate(new Vector3(0, -150 * Time.deltaTime, 0));
            yield return null;
        }
        Invoke("LevelCompleted", 1.5f);
    }

    private void CanvasActivate()
    {
        for (int n = 0; n < transform.childCount; n++)
        {
            var child = transform.GetChild(n);
            if (!child.gameObject.activeSelf)
                child.gameObject.SetActive(true);
        }
    }

    private IEnumerator CameraIntro()
    {
        GameObject camera = GameObject.Find("Main Camera");
        var angle = 0f;
        while (angle < 3.5f)
        {
            angle += Time.deltaTime;
            var x = Mathf.Cos(angle * 0.5f) * 67f;
            var y = Mathf.Sin(angle * 0.5f) * 67f;
            camera.transform.position = new Vector3(16.1f, y, -x);
            Quaternion direction = Quaternion.FromToRotation(camera.transform.forward, (new Vector3(16.1f, 1, 13.9f)) - camera.transform.position);
            camera.transform.rotation = Quaternion.Lerp(camera.transform.rotation, direction * camera.transform.rotation, 10);
            yield return new WaitForEndOfFrame();
        }
    }
}
