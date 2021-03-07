using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyShield : MonoBehaviour
{
    private GameObject energyShield;
    private Health health;
    private PrefabsConteiner prefabsConteiner;
    private float startImmortalityTime = 2f, buffImmortalityTime = 10f, defenderBuffTime = 5f, timer;
    private Coroutine shield;

    private void Awake()
    {
        health = GetComponent<Health>();
    }
    public void StartImmortality()
    {
        if (shield != null)
        {
            StopCoroutine(shield);
            energyShield.SetActive(false);
        }
        shield = StartCoroutine(ImmortalityRoutin(startImmortalityTime));
    }
    public void BuffImmortality()
    {
        if (shield != null)
        {
            StopCoroutine(shield);
            energyShield.SetActive(false);
        }
        shield = StartCoroutine(ImmortalityRoutin(buffImmortalityTime));
    }
    public void PlayerDefenderBuff()
    {
        if (shield != null)
        {
            StopCoroutine(shield);
            energyShield.SetActive(false);
        }
        shield = StartCoroutine(ImmortalityRoutin(defenderBuffTime));
    }
    public void CustomImmortality(float time)
    {
        if (shield != null)
        {
            StopCoroutine(shield);
            energyShield.SetActive(false);
        }
        shield = StartCoroutine(ImmortalityRoutin(time));
    }
    private IEnumerator ImmortalityRoutin(float timeOfImmortality)
    {
        timer = timeOfImmortality;
        energyShield =  TotalSpawner.spawn.SpawnFromSpawner("EnergyShield",new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z), Quaternion.identity);
        health.SetImmortality();
        while (timer >= 0)
        {
            timer -= Time.deltaTime;
            energyShield.transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
            yield return null;
        }
        energyShield.SetActive(false);
        health.SetMortality();
    }

    private void OnDisable()
    {
        energyShield.SetActive(false);
    }
}
