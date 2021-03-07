using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_2 : Player
{
    private void Update()
    {
        PlayersLife();
    }


    private void PlayersLife()
    {
        if (alive)
        {
           // Timer();
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
