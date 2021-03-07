using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapCreator : MonoBehaviour
{
    [SerializeField] protected GameObject baseSpawnObjects;
    [SerializeField] private LayerMask layers;
    [SerializeField] private Constructor TileNull;
    [SerializeField] private List<Constructor> TilePrefabs;


    private BaseSpawnPlacer[] baseObjects;
    private DeathRedWall [] baseBricks;
    private List<Constructor> existingTiles; // нужно инициализировать



    private void Start()
    {
        baseObjects = baseSpawnObjects.GetComponentsInChildren<BaseSpawnPlacer>();
        baseBricks = baseSpawnObjects.GetComponentsInChildren<DeathRedWall>();
        NewConstruct();
    }

    protected void NewConstruct()
    {
            StartCheck();
            // Instantiate(baseSpawnObjects);
            baseSpawnObjects.SetActive(true);


            foreach (var item in baseObjects)
            {
                item.CheckPosition(layers);
            }

            ReturnBaseBricks();
            Assistent.assist.DiggerSetup();

        
    }

    private void ReturnBaseBricks()
    {
        foreach (var item in baseBricks)
        {
            item.gameObject.SetActive(true);
        }
    }

    private void StartCheck()
    {
        existingTiles = new List<Constructor>();
        GameObject Level = new GameObject("Level");

        const float X = -7.5f;
        const float h = 4f;
        const float Y = 2.5f;
        float newX;
        float newY;

        int counter = 0;

        for (int i = 0; i < 7; i++)
        {
            newY = Y + (h * i);
            for (int j = 0; j < 10; j++)
            {
                newX = X + (h * j);

                if (counter == 0)
                {
                    //ставим совсем рандомный тайл
                    var tile = GetRandomTile(TilePrefabs);
                    //var construct = tile.GetComponent<Constructor>();
                    existingTiles.Add(tile);
                    Instantiate(tile.gameObject, new Vector3(newX, 0, newY), Quaternion.identity, Level.transform);
                }

                else if (counter > 9 && counter % 10 != 0)
                {
                    //делаем проверку после десятого тайла
                    var tile = ChoosePossibleTiles(existingTiles[counter - 1], existingTiles[counter - 10]);

                    if (tile == null) tile = TileNull;

                    existingTiles.Add(tile);
                    Instantiate(tile.gameObject, new Vector3(newX, 0, newY), Quaternion.identity, Level.transform);
                }

                else if (counter > 9 && counter % 10 == 0)
                {
                    //первый тайл каждого ряда
                    var tile = ChoosePossibleTiles(null, existingTiles[counter - 10]);

                    if (tile == null) tile = TileNull;

                    existingTiles.Add(tile);
                    Instantiate(tile.gameObject, new Vector3(newX, 0, newY), Quaternion.identity, Level.transform);
                }

                else
                {
                    //делаем проверку от вторго тайла до десятого
                    var tile = ChoosePossibleTiles(existingTiles[counter - 1]);

                    if (tile == null) tile = TileNull;

                    existingTiles.Add(tile);
                    Instantiate(tile.gameObject, new Vector3(newX, 0, newY), Quaternion.identity, Level.transform);
                }

                counter++;
            }
        }
    }

    private Constructor ChoosePossibleTiles(Constructor byLeftSide, Constructor byDownSide = null)
    {
        List<Constructor> possibleTiles = new List<Constructor>();

        //получаем ссылки на enum проверяемых тайлов. Нужны правый и верхний, так как они контачат со следующим левой и нижней сторонами. 
        var tileRightSide = Constructor.TileSide.Right;
        var tileTopSide = Constructor.TileSide.Top;
        var tileLeftSide = Constructor.TileSide.Left;
        var tileDownSide = Constructor.TileSide.Down;

        if (byLeftSide != null)
        {
           // получаем ID правой стороны тайла, который лежит слева от нас (предыдущий тайл)
             var leftID = byLeftSide.TileID(tileRightSide);

           // создаём перечень всех возможных тайлов, которые подходят по левой стороне предыдущему тайлу
            foreach (var leftsideTile in TilePrefabs)
            {
               var ID = leftsideTile.TileID(tileLeftSide);
               if (ID == leftID)
               {
                  possibleTiles.Add(leftsideTile);
               }
            }
        }
        else
        {
            possibleTiles = TilePrefabs;
        }
       

        //необходимо для проверки тайлов второго ряда и выше, подходят ли они нижним тайлам
        if (byDownSide != null)
        {
            List<Constructor> possibleTiles_compleate = new List<Constructor>();
            var downID = byDownSide.TileID(tileTopSide);

            foreach (var downSideTile in possibleTiles)
            {
                var ID = downSideTile.TileID(tileDownSide);
                if (ID == downID)
                {
                    possibleTiles_compleate.Add(downSideTile);
                }
            }

            possibleTiles = possibleTiles_compleate;
        }
        if (possibleTiles.Count == 0)
        {
            var randomTile = GetRandomTile(TilePrefabs);
            return randomTile;
        }

        var finalTile = GetRandomTile(possibleTiles);
        return finalTile;
    }

    private Constructor GetRandomTile(List<Constructor> collection)
    {
        Constructor random = collection[Random.Range(0, collection.Count)];
        return random;
    }
}


