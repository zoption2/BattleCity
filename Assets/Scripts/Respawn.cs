using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public bool isPointBusy;
    public bool canRespNow;

    [SerializeField] private bool isPlayer;
    private ParticleSystem flareEffect;
    private PrefabsConteiner prefabsConteiner;
    private GameplayManager GPM;
    private Collider myCollider;
    private List<GameObject> _tanksAtPoint;
    private bool needCheckPointClear;


    private void Start()
    {
        flareEffect = GetComponentInChildren<ParticleSystem>();
        // prefabsConteiner = GameObject.Find("PrefabConteiner").GetComponent<PrefabsConteiner>();
        prefabsConteiner = PrefabsConteiner.instance;
        GPM = GameObject.Find("Canvas").GetComponent<GameplayManager>();
        myCollider = GetComponent<Collider>();
        isPointBusy = false;
        canRespNow = true;
        _tanksAtPoint = new List<GameObject>();
        needCheckPointClear = false;
    }

    private void Update()
    {
        if (needCheckPointClear)
        {
           PointAreClear();
        }
        
    }

    public void AddTankAtPoint(GameObject tank)
    {
        _tanksAtPoint.Add(tank);
        PointAreClear();
    }
    public void DeleteTankFromPoint(GameObject tank)
    {
        _tanksAtPoint.Remove(tank);
        PointAreClear();
    }
    private bool PointAreClear()
    {
        ClearDeadList();
        if (_tanksAtPoint.Count == 0)
        {
            return true;
        }
        else return false;
    }

    private void ClearDeadList()
    {
        foreach (var item in _tanksAtPoint)
        {
            if (item == null)
            {
                _tanksAtPoint.Remove(item);
            }
        }
    }

    public void SpawnEnemy()
    {
        StartCoroutine(_spawnEnemy());
    }
    public void SpawnPlayer(int player_number)
    {
        StartCoroutine(_spawnPlayer(player_number));
    }

    private IEnumerator _spawnEnemy()
    {
        // spawn enemy effect
        needCheckPointClear = true;
        isPointBusy = true;
        flareEffect.Play();
        yield return new WaitForSeconds(1.9f);
        yield return new WaitUntil(() => PointAreClear());
        flareEffect.Stop(true);

        //choosing of spawning enemy
        List<GameObject> enemyToSpawn = new List<GameObject>();
        enemyToSpawn.Clear();
        if (LevelManager.smallEnemy > 0) enemyToSpawn.Add(prefabsConteiner.smallEnemy);
        if (LevelManager.fastEnemy > 0) enemyToSpawn.Add(prefabsConteiner.fastEnemy);
        if (LevelManager.bigEnemy > 0) enemyToSpawn.Add(prefabsConteiner.bigEnemy);
        if (LevelManager.armoredEnemy > 0) enemyToSpawn.Add(prefabsConteiner.armoredEnemy);
        if (LevelManager.bossEnemy > 0) enemyToSpawn.Add(prefabsConteiner.bossEnemy);

        GameObject enemyName = enemyToSpawn[Random.Range(0, enemyToSpawn.Count)];
        GameObject enemy = Instantiate(enemyName, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), Quaternion.identity);
       
        //adding of BuffDealer effect to enemy with some chance
        if (Randomizer(LevelManager.buffSpawnRate)) enemy.AddComponent<BuffDealer>();


        if (enemyName == prefabsConteiner.smallEnemy) LevelManager.smallEnemy--;
        else if (enemyName == prefabsConteiner.fastEnemy) LevelManager.fastEnemy--;
        else if (enemyName == prefabsConteiner.bigEnemy) LevelManager.bigEnemy--;
        else if (enemyName == prefabsConteiner.armoredEnemy) LevelManager.armoredEnemy--;
        else if (enemyName == prefabsConteiner.bossEnemy) LevelManager.bossEnemy--;

        isPointBusy = false;
        needCheckPointClear = false;
    }
   
    private IEnumerator _spawnPlayer (int player_number) 
    {
        //effect of spawning
        needCheckPointClear = true;
        flareEffect.Play();
        yield return new WaitForSeconds(1.9f);
        yield return new WaitUntil(() => PointAreClear());
        flareEffect.Stop(true);

        //choose player to spawn
        switch (player_number)
        {
            case 1:
                GameObject player1 = Instantiate(prefabsConteiner.player_1, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), Quaternion.identity);
                break;
            case 2:
                GameObject player2 = Instantiate(prefabsConteiner.player_2, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), Quaternion.identity);
                break;
            case 3:
                GameObject player3 = Instantiate(prefabsConteiner.player_3, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), Quaternion.identity);
                break;
            case 4:
                GameObject player4 = Instantiate(prefabsConteiner.player_4, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), Quaternion.identity);
                break;
            default:
                Debug.LogError("Only 4 players avalible for choose. Enter INT between 1 and 4.");
                break;
        }

        needCheckPointClear = false;
    }

    public bool Randomizer(float buffSpawnRate)
    {
        float randomePick = Random.Range(1, 100);
        if (randomePick <= buffSpawnRate)
        {
            return true;
        }
        else return false;
    }
}
