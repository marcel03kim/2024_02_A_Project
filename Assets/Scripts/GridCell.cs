using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell
{
    public Vector3Int Position;
    public bool isOccupied;
    public GameObject Building;

    public GridCell(Vector3Int position)
    {
        Position = position;
        isOccupied = false;
        Building = null;
    }
}
