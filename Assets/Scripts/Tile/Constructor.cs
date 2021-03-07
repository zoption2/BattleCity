using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constructor : MonoBehaviour
{
    public enum TileSide { Left, Top, Right, Down }

    [Header("Tiles side's ID ")]
    [Space]
    [Tooltip("0 - free, 1 - RedWall, 2 - SteelWall, 3 - Water, 4 - Tree, 5 - Swamp")]

    [SerializeField] private int leftSideID;
    [SerializeField] private int topSideID;
    [SerializeField] private int rightSideID;
    [SerializeField] private int downSideID;

    public int TileID(TileSide tileSide)
    {
        switch (tileSide)
        {
            case TileSide.Left:
                return leftSideID;
            case TileSide.Top:
                return topSideID;
            case TileSide.Right:
                return rightSideID;
            case TileSide.Down:
                return downSideID;
            default:
                return 0000;
        }
    }
}
