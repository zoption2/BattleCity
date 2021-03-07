using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Rank
{
    public static string __RankOn(string playerTag)
    {
        if (MasterController.playerScores[playerTag] <= 9999) return Language.GetText("rank.0");
        else if (MasterController.playerScores[playerTag] >= 10000 && MasterController.playerScores[playerTag] <= 19999) return Language.GetText("rank.1");
        else if (MasterController.playerScores[playerTag] >= 20000 && MasterController.playerScores[playerTag] <= 29999) return Language.GetText("rank.2");
        else if (MasterController.playerScores[playerTag] >= 30000 && MasterController.playerScores[playerTag] <= 39999) return Language.GetText("rank.3");
        else if (MasterController.playerScores[playerTag] >= 40000 && MasterController.playerScores[playerTag] <= 49999) return Language.GetText("rank.4");
        else if (MasterController.playerScores[playerTag] >= 50000 && MasterController.playerScores[playerTag] <= 59999) return Language.GetText("rank.5");
        else if (MasterController.playerScores[playerTag] >= 60000 && MasterController.playerScores[playerTag] <= 69999) return Language.GetText("rank.6");
        else if (MasterController.playerScores[playerTag] >= 70000 && MasterController.playerScores[playerTag] <= 79999) return Language.GetText("rank.7");
        else if (MasterController.playerScores[playerTag] >= 80000 && MasterController.playerScores[playerTag] <= 89999) return Language.GetText("rank.8");
        else if (MasterController.playerScores[playerTag] >= 90000 && MasterController.playerScores[playerTag] <= 99999) return Language.GetText("rank.9");
        else if (MasterController.playerScores[playerTag] >= 100000 && MasterController.playerScores[playerTag] <= 109999) return Language.GetText("rank.10");
        else if (MasterController.playerScores[playerTag] >= 110000 && MasterController.playerScores[playerTag] <= 119999) return Language.GetText("rank.11");
        else if (MasterController.playerScores[playerTag] >= 120000 && MasterController.playerScores[playerTag] <= 129999) return Language.GetText("rank.12");
        else if (MasterController.playerScores[playerTag] >= 130000 && MasterController.playerScores[playerTag] <= 139999) return Language.GetText("rank.13");
        else if (MasterController.playerScores[playerTag] >= 140000 && MasterController.playerScores[playerTag] <= 149999) return Language.GetText("rank.14");
        else if (MasterController.playerScores[playerTag] >= 150000 && MasterController.playerScores[playerTag] <= 159999) return Language.GetText("rank.15");
        else if (MasterController.playerScores[playerTag] >= 160000 && MasterController.playerScores[playerTag] <= 169999) return Language.GetText("rank.16");
        else if (MasterController.playerScores[playerTag] >= 170000 && MasterController.playerScores[playerTag] <= 179999) return Language.GetText("rank.17");
        else if (MasterController.playerScores[playerTag] >= 180000 && MasterController.playerScores[playerTag] <= 189999) return Language.GetText("rank.18");
        else if (MasterController.playerScores[playerTag] >= 190000 && MasterController.playerScores[playerTag] <= 199999) return Language.GetText("rank.19");
        else if (MasterController.playerScores[playerTag] >= 200000 && MasterController.playerScores[playerTag] <= 209999) return Language.GetText("rank.20");
        else if (MasterController.playerScores[playerTag] >= 210000 && MasterController.playerScores[playerTag] <= 219999) return Language.GetText("rank.21");
        else if (MasterController.playerScores[playerTag] >= 220000 ) return Language.GetText("rank.22");
        else return null;
    }

    public static string GetRankCode(string playerTag)
    {
        int currentScores = MasterController.playerScores[playerTag];
        int position = currentScores / 10000;
        string rank = "rank." + position.ToString();
        return rank;
    }

    public static string GetRankByCode(string rankCode)
    {
        return Language.GetText(rankCode);
    }

    public static string RankOn (string playerTag)
    {
        string rankCode = GetRankCode(playerTag);
        return Language.GetText(rankCode);
    }
    
}
