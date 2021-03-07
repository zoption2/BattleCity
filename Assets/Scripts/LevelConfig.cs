using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConfig : MonoBehaviour
{
    public static LevelConfig Instance;
    private static Dictionary<int, Config> configsConteiner = new Dictionary<int, Config>();




    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
        //Config lvl1 = new Config { smallEnemies = 20, fastEnemies = 10, bigEnemies = 5, armoredEnemies = 5 };
        //configsConteiner.Add(1, lvl1);

        //Config lvl2 = new Config { smallEnemies = 20, fastEnemies = 10, bigEnemies = 5, armoredEnemies = 5 };
        //configsConteiner.Add(2, lvl2);

        //Config lvl3 = new Config { smallEnemies = 40, fastEnemies = 20, bigEnemies = 10, armoredEnemies = 10 };
        //configsConteiner.Add(3, lvl3);

        //Config lvl4 = new Config { smallEnemies = 40, fastEnemies = 20, bigEnemies = 10, armoredEnemies = 10 };
        //configsConteiner.Add(4, lvl4);

        //Config lvl5 = new Config { smallEnemies = 40, fastEnemies = 20, bigEnemies = 10, armoredEnemies = 10 };
        //configsConteiner.Add(5, lvl5);

        //Config lvl6 = new Config { smallEnemies = 40, fastEnemies = 20, bigEnemies = 10, armoredEnemies = 10 };
        //configsConteiner.Add(5, lvl6);

        //for (int i = 1; i < 100; i++)
        //{
        //    Config lvl = new Config { smallEnemies = 20, fastEnemies = 10, bigEnemies = 5, armoredEnemies = 5 };
        //    configsConteiner.Add(i, lvl);
        //}
    }

    public void CreateLevelStats()
    {
        configsConteiner = new Dictionary<int, Config>();

        int playerCount = MasterController.totalPlayersInGame;
        int _smallEnemies = 0;
        int _fastEnemies = 0;
        int _bigEnemies = 0;
        int _armoredEnemies = 0;
        int _countOfEnemiesAtScreen = 4;
        float _buffDealerSpawnRate = 15;

        float multiplier = 0;

        for (int i = 1; i < 100; i++)
        {
            switch (playerCount)
            {
                case 1:
                    _smallEnemies = 15;
                    _fastEnemies = 10;
                    _bigEnemies = 5;
                    _armoredEnemies = 5;
                    _buffDealerSpawnRate = 30;
                    multiplier = 0.2f;
                    break;
                //case 1:
                //    _smallEnemies = 3;
                //    _fastEnemies = 1;
                //    _bigEnemies = 0;
                //    _armoredEnemies = 0;
                //    _buffDealerSpawnRate = 30;
                //    multiplier = 0.2f;
                //    break;

                case 2:
                    _smallEnemies = 20;
                    _fastEnemies = 15;
                    _bigEnemies = 10;
                    _armoredEnemies = 5;
                    _buffDealerSpawnRate = 25;
                    multiplier = 0.25f;
                    break;
              
                case 3:
                    _smallEnemies = 25;
                    _fastEnemies = 15;
                    _bigEnemies = 10;
                    _armoredEnemies = 5;
                    _buffDealerSpawnRate = 20;
                    multiplier = 0.35f;
                    break;
               
                case 4:
                    _smallEnemies = 25;
                    _fastEnemies = 15;
                    _bigEnemies = 10;
                    _armoredEnemies = 10;
                    _buffDealerSpawnRate = 15;
                    multiplier = 0.5f;
                    break;
                default:
                    break;
            }

            Config lvl = new Config { smallEnemies = _smallEnemies + Mathf.RoundToInt(multiplier * i), fastEnemies = _fastEnemies + Mathf.RoundToInt(multiplier * i), bigEnemies = _bigEnemies + Mathf.RoundToInt(multiplier * i), armoredEnemies = _armoredEnemies + Mathf.RoundToInt(multiplier * i), buffDealerSpawnRate = _buffDealerSpawnRate };
            configsConteiner.Add(i, lvl);

        }


}

    public Config Configs(int lvl)
    {
        var conf = configsConteiner[lvl];
        return conf;
    }

}

public class Config
{
    public int smallEnemies = 0;
    public int fastEnemies = 0;
    public int bigEnemies = 0;
    public int armoredEnemies = 0;
    public int stageNumber;
    public float spawnRate = 2.1f;
    public int countOfEnemiesAtScreen = 4;
    public float buffDealerSpawnRate = 15;
}


