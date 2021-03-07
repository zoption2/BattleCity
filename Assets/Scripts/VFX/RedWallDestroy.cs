using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedWallDestroy : MonoBehaviour
{
    private SoundManager soundManager;

    private void Start()
    {
        soundManager = SoundManager.play;
    }
    private void OnDisable()
    {
        if (!MasterController.stageClear)
        {
            string sound = soundManager.hitBricksSound();
            soundManager.PlayShortAudio(sound, 0.15f, true);
        } 
        TotalSpawner.spawn.SpawnFromSpawner("RedWallDestroy", transform.position, Quaternion.identity);
    }
}
