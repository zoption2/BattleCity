using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Assistent : MonoBehaviour
{
    public static Assistent assist;

    private GameObject BaseBricks;
    private GameObject red;
    private GameObject steel;
    private GameObject[] allRed;
    private GameObject[] allSteel;

    private GameObject buffAtScene;

    private void Awake()
    {
        if (assist == null)
        {
            assist = this;
        }
        else Destroy(this);

        ClearBuffsFromStage();
    }

    private void OnDisable()
    {
        ClearBuffsFromStage();
    }


    private Coroutine digger;
    public void AssistDiggerBuff()
    {
        if (digger != null)
        {
            StopCoroutine(digger);
        }
        digger = StartCoroutine(DiggerBuffTime());
    }

    private void ClearBuffsFromStage()
    {
        buffAtScene?.SetActive(false);
    }


    public void DiggerSetup()
    {
        BaseBricks = GameObject.Find("BaseBricks");

        red = BaseBricks.transform.Find("Red").gameObject;
        steel = BaseBricks.transform.Find("Steel").gameObject;

        //массив кирпичей вокруг базы
        Transform[] reds = red.GetComponentsInChildren<Transform>();
        int countRed = reds.Length;
        allRed = new GameObject[countRed];
        for (int i = 0; i < countRed; i++)
        {
            allRed[i] = reds[i].gameObject;
        }

        //массив стальных блоков вокруг базы
        int countSteel = steel.transform.childCount;
        allSteel = new GameObject[countSteel];
        for (int i = 0; i < countSteel; i++)
        {
            allSteel[i] = steel.transform.GetChild(i).gameObject;
        }
    }

    private IEnumerator DiggerBuffTime()
    {
        int lastSeconds = 5;

        //включаем все стальные блоки на базе, выключаем кирпичи
        steel.SetActive(true);
        red.SetActive(false);
        foreach (var item in allSteel)
        {
            item.SetActive(true);
        }

        yield return new WaitForSeconds(25f);
        
        while (lastSeconds > 0)
        {
            steel.SetActive(false);
            red.SetActive(true);
            yield return new WaitForSeconds(0.5f);

            steel.SetActive(true);
            red.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            lastSeconds -= 1;
        }

        //конец действия баффа, все кирпичи восстановлены, стальные блоки пропадают

        steel.SetActive(false);
        red.SetActive(true);
        foreach (var item in allRed)
        {
            item.SetActive(true);
        }
    }

    private Coroutine timeBuff;
    public void AssistTimerBuff()
    {
        if (timeBuff != null)
        {
            StopCoroutine(timeBuff);
        }
        timeBuff = StartCoroutine(TimerBuffTime());
    }

    public void SummonBuff()
    {
        buffAtScene?.SetActive(false);

        var currentBuff = MasterController.buffs[Random.Range(0, MasterController.buffs.Count)];
        buffAtScene = TotalSpawner.spawn.SpawnFromSpawner(currentBuff, new Vector3(newCoord(-9, 29), transform.position.y + 1.1f, newCoord(1, 27)), Quaternion.Euler(0, 180, 0));
    }

    private int newCoord(int min, int max)
    {
        int coord = Random.Range(min, max);
        return coord;
    }

    private IEnumerator TimerBuffTime()
    {
        List<GameObject> enemies = new List<GameObject>();
        enemies = LevelManager.Instance.GetAllEnemies();
        foreach (var enemy in enemies)
        {
            var ai = enemy.GetComponent<EnemyAI>();
            ai.enemyLife = EnemyLife.freeze;
        }
        yield return new WaitForSeconds(20f);
       
        foreach (var enemy in enemies)
        {
            var ai = enemy.GetComponent<EnemyAI>();
            ai.enemyLife = EnemyLife.alive;
        }
    }
}
