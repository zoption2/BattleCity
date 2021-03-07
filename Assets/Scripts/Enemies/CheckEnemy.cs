using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemy : MonoBehaviour
{
    private LevelManager levelManager;
    private GameObject enemy;

    private void Start()
    {
        levelManager = LevelManager.Instance;
        enemy = this.gameObject;

        levelManager.SetEnemy(enemy);
    }
}
