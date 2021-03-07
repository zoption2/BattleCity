using System.Collections.Generic;
using UnityEngine;


public static class PrepareForGame 
{
    public static Dictionary<int, bool> allPlayers_Ready;

    public static List<GameObject> eventControllers;
    public static List<string> playerRegister;

    public static CharacterMenu CharacterMenu;

    public static void DoNewGame()
    {
        allPlayers_Ready = new Dictionary<int, bool>();
        eventControllers = new List<GameObject>();
        playerRegister = new List<string>();
    }

    public static void SetCharacterMenu(CharacterMenu menu)
    {
        CharacterMenu = menu;
    }

    public static void AddPlayer(int number, bool isReady)
    {
        if (!allPlayers_Ready.ContainsKey(number))
        allPlayers_Ready.Add(number, isReady);
    }
    public static bool AllPlayers_ready()
    {
        foreach (var item in allPlayers_Ready)
        {
            if (item.Value == false)
            {
                return false;
            }
        }

        CharacterMenu.DoCountdown();
        
        return true;
    }
}
