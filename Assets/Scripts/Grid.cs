using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private int[,] _map;
    private int _rows;
    private int _cols;

    public Grid(int[,] map)
    {
        _map = map;
        _rows = _map.GetLength(0);
        _cols = _map.GetLength(1);
    }

    public bool IsPassable(int x, int y)
    {
        return _map[x, y] == 0;
    }

    public int Rows => _rows;
    public int Cols => _cols;
}
