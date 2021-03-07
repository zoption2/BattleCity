using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player_1 : Player
{
    private void Update()
    {
        PlayersLife();
        if (Input.GetKeyDown(KeyCode.Z))  PlayersStats.Instance.SetStars(_thisPlayer, 1);
        else if (Input.GetKeyDown(KeyCode.X)) PlayersStats.Instance.SetStars(_thisPlayer, -1);

        if (Input.GetKeyDown(KeyCode.C)) PlayersStats.Instance.SetProjectiles(_thisPlayer, 1);
    }


    private void PlayersLife()
    {
        if (alive)
        {
            //shoot.Timer();
        }
        else
        {
            gameObject.GetComponent<Control>().enabled = false;
            transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
        }
    }

    public override void Shoot()
    {
        shoot.AutoShoot();
    }
    public override void UseFirstSkill()
    {
        spec.FirstSkill(_thisPlayer);
    }

    public override void UseSecondSkill()
    {
        spec.SecondSkill(_thisPlayer);
    }

    public override void UseThirdSkill()
    {
        spec.ThirdSkill(_thisPlayer);
    }
}
