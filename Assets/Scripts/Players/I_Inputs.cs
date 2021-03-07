using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_Inputs
{
    float MoveHorizontal();
    float MoveVertical();
    bool Shoot();
    bool LaunchRocket();
    bool PlantMine();
    bool NucleMissile();
}


public class PlayerOne : MonoBehaviour, I_Inputs
{
    public float MoveHorizontal()
    {
        var right = Input.GetAxis("Horizontal1");
        return right;
    }
   public float MoveVertical()
    {
        var forward = Input.GetAxis("Vertical1");
        return forward;
    }

    public bool Shoot()
    {
        var shoot = Input.GetKeyDown(KeyCode.Joystick1Button0);
        return shoot;
    }
    public bool LaunchRocket()
    {
        var launch = Input.GetKeyDown(KeyCode.Joystick1Button1);
        return launch;
    }

    public bool PlantMine()
    {
        var launch = Input.GetKeyDown(KeyCode.Joystick1Button3);
        return launch;
    }

    public bool NucleMissile()
    {
        var launch = Input.GetKeyDown(KeyCode.Joystick1Button2);
        return launch;
    }
}

public class PlayerTwo : MonoBehaviour, I_Inputs
{
    public float MoveHorizontal()
    {
        var right = Input.GetAxisRaw("Horizontal2");
        return right;
    }
    public float MoveVertical()
    {
        var forward = Input.GetAxisRaw("Vertical2");
        return forward;
    }

    public bool Shoot()
    {
        var shoot = Input.GetKeyDown(KeyCode.Joystick2Button0);
        return shoot;
    }
    public bool LaunchRocket()
    {
        var launch = Input.GetKeyDown(KeyCode.Joystick2Button1);
        return launch;
    }
    public bool PlantMine()
    {
        var launch = Input.GetKeyDown(KeyCode.Joystick1Button3);
        return launch;
    }
    public bool NucleMissile()
    {
        var launch = Input.GetKeyDown(KeyCode.Joystick1Button2);
        return launch;
    }
}

public class PlayerThree : MonoBehaviour, I_Inputs
{
    public float MoveHorizontal()
    {
        var right = Input.GetAxisRaw("Horizontal3");
        return right;
    }
    public float MoveVertical()
    {
        var forward = Input.GetAxisRaw("Vertical3");
        return forward;
    }

    public bool Shoot()
    {
        var shoot = Input.GetKeyDown(KeyCode.Joystick3Button0);
        return shoot;
    }
    public bool LaunchRocket()
    {
        var launch = Input.GetKeyDown(KeyCode.Joystick3Button1);
        return launch;
    }
    public bool PlantMine()
    {
        var launch = Input.GetKeyDown(KeyCode.Joystick1Button3);
        return launch;
    }
    public bool NucleMissile()
    {
        var launch = Input.GetKeyDown(KeyCode.Joystick1Button2);
        return launch;
    }
}
public class PlayerFour : MonoBehaviour, I_Inputs
{
    public float MoveHorizontal()
    {
        var right = Input.GetAxisRaw("Horizontal4");
        return right;
    }
    public float MoveVertical()
    {
        var forward = Input.GetAxisRaw("Vertical4");
        return forward;
    }

    public bool Shoot()
    {
        var shoot = Input.GetKeyDown(KeyCode.Joystick4Button0);
        return shoot;
    }
    public bool LaunchRocket()
    {
        var launch = Input.GetKeyDown(KeyCode.Joystick4Button1);
        return launch;
    }
    public bool PlantMine()
    {
        var launch = Input.GetKeyDown(KeyCode.Joystick1Button3);
        return launch;
    }
    public bool NucleMissile()
    {
        var launch = Input.GetKeyDown(KeyCode.Joystick1Button2);
        return launch;
    }
}

