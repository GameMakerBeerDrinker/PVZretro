using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Cell
{ 
    public enum CellType
    {
        Invalid,
        Grass,
        Grave,
        Water
    }

    public CellType cellType;

    public GameObject plant;

    public Vector2Int position;
}
