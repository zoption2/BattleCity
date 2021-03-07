using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPlayerSkillsControl : MonoBehaviour
{
    private int addedStars;
    private string myName;
    private Coroutine _starsBuff;
    private PlayersStats playersStats;
    private VFXTotalSpawner vFX;
    private GameplayManager GPM;

    void Start()
    {
        myName = gameObject.tag;
        playersStats = PlayersStats.Instance;
        vFX = VFXTotalSpawner.Instance;
        GPM = GameObject.Find("Canvas").GetComponent<GameplayManager>();

        GPM.OnLevelCompleated += ReturnStars;
    }


    public void UseSupportStars()
    {
        if (_starsBuff != null)
        {
            StopCoroutine(supportStars());
            ReturnStars();
            //MasterController.playersUnderStarBuff[myName] = false; 
        }
        _starsBuff = StartCoroutine(supportStars());
    }

    private IEnumerator supportStars()
    {
        if (MasterController.playersUnderStarBuff[myName] == false)
        {
            int currentStars = playersStats.GetStars(myName);
            int needToBeAddedStars = Mathf.Clamp(5 - currentStars, 0, 2);
            addedStars = needToBeAddedStars;
            MasterController.playersUnderStarBuff[myName] = true;
            playersStats.BuffStars(myName, addedStars);

            vFX.PlayEffect("ButtleShine", transform.position, Quaternion.identity, 10f, transform);

            yield return new WaitForSeconds(10f);

            ReturnStars();
        }
    }

    private void ReturnStars()
    {
        if (MasterController.playersUnderStarBuff[myName] == true)
        {
            playersStats.SetStars(myName, -addedStars);
            addedStars = 0;
            MasterController.playersUnderStarBuff[myName] = false;
        }
    }

    private void OnDisable()
    {
        ReturnStars();
        GPM.OnLevelCompleated -= ReturnStars;
    }
}
