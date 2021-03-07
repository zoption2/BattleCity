using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StarsBoost
{
    public static float SpeedModifier(string player)
    {
        switch (MasterController.playerStars[player])
        {
            case 0: return 1;
            case 1: return 1.5f;
            case 2: return 1.6f;
            case 3: return 1.7f;
            case 4: return 1.8f;
            case 5: return 2f;
            default: return 2f;
        }
    }

    public static int MultiShoot(string player)
    {
        switch (MasterController.playerStars[player])
        {
            case 0: return 1;
            case 1: return 1;
            case 2: return 2;
            case 3: return 2;
            case 4: return 2;
            case 5: return 3;
            default: return 3;
        }
    }

    public static float PrepareTime(string player)
    {
        switch (MasterController.playerStars[player])
        {
            case 0: return 0.4f;
            case 1: return 0.35f;
            case 2: return 0.32f;
            case 3: return 0.3f;
            case 4: return 0.27f;
            case 5: return 0.24f;
            default: return 0.24f;
        }
    }

    public static bool CanDestroySteel(string player)
    {
        switch (MasterController.playerStars[player])
        {
            case 0: return false;
            case 1: return false;
            case 2: return false;
            case 3: return true;
            case 4: return true;
            case 5: return true;
            default: return true;
        }
    }
}
