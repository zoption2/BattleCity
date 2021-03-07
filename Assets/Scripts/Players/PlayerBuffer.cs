using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerBuffer : MonoBehaviour, I_PlayerSpecific
{
    private string _thisPlayer;
    private void Start()
    {
        _thisPlayer = this.gameObject.tag;
        if (!Conteiner.starsAssist.ContainsKey(_thisPlayer))
        {
            Conteiner.starsAssist.Add(_thisPlayer, 0);
        }
    }
    public void FirstSkill(string playerName)
    {
        if (MasterController.playerBoosters[playerName] > 0)
        {
            RaycastHit hit;
            int leyerMask = 1 << 16;
            var getTarget = Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.forward, out hit, Mathf.Infinity, leyerMask);
            Debug.DrawRay(transform.position, transform.forward, Color.green);
            if (getTarget)
            {
                if (hit.transform.gameObject.GetComponent<I_Buffs>() != null)
                {
                    StartCoroutine(GrabTheBuff(hit.transform));
                    //MasterController.playerBoosters[playerName]--;
                    PlayersStats.Instance.SetProjectiles(playerName, -1);
                }
            }
        }
    }

    private IEnumerator GrabTheBuff(Transform buffTransform)
    {
        while (buffTransform.gameObject.activeSelf && buffTransform.position != this.transform.position)
        {
            var trans = Vector3.MoveTowards(buffTransform.position, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), 10f * Time.deltaTime);
            buffTransform.position = trans;
            yield return new WaitForFixedUpdate();
        }
    }

    public void SecondSkill(string playerName)
    {
        if (MasterController.playerBoosters[playerName] > 1)
        {
            Assistent.assist.SummonBuff();
            //MasterController.playerBoosters[playerName] -=2;
            PlayersStats.Instance.SetProjectiles(playerName, -2);
        }
    }

    public void ThirdSkill(string playerName)
    {
        if (MasterController.playerBoosters[playerName] > 2 && MasterController.playerStars[playerName] > 0)
        {
            PlayersStats.Instance.SetProjectiles(playerName, -3);
            //Assistent.assist.SetStars(playerName, -1);
            PlayersStats.Instance.SetStars(playerName, -1);
            int count = 1;

            for (int i = 1; i <= MasterController.totalPlayersInGame; i++)
            {
                string _gamer = "Player_" + i;
                if(MasterController.playerStars[_gamer] < 5 && _gamer != playerName)
                {
                    //Assistent.assist.SetStars(_gamer, 1);
                    MasterController.bonusScores[playerName] += 500;
                    count += 1;

                    PlayersStats.Instance.SetStars(_gamer, 1);
                    Conteiner.starsAssist[_thisPlayer]++;
                }
            }

            BonusPoints.ShowPoints(500 * count, new Vector3(transform.position.x, 3, transform.position.z));
        }
    }

    protected void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<Buffs>() != null)
        {
            collider.gameObject.GetComponent<Buffs>().BuffMe(gameObject);
        }
    }
}
