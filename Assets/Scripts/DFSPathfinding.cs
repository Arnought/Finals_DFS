using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DFSPathfinding : MonoBehaviour
{
    private Grid _grid;
    public GameObject startPrefab;
    public GameObject targetPrefab;
    public GameObject obstaclePrefab;
    public GameObject pathPrefab;
    public GameObject groundPrefab;
    public GameObject playerPrefab;

    void Start()
    {
        // Initialize a sample map (5x5 grid)
        int[,] map = new int[,] {
            { 0, 0, 1, 0, 0 },
            { 0, 1, 0, 1, 0 },
            { 0, 0, 0, 1, 0 },
            { 1, 1, 0, 0, 0 },
            { 0, 0, 0, 1, 0 }
        };

        _grid = new Grid(map);

        Vector2Int start = new Vector2Int(0, 0);
        Vector2Int target = new Vector2Int(4, 4);

        DrawGrid();
        Instantiate(startPrefab, new Vector3(start.x, 0, start.y), Quaternion.identity);
        Instantiate(targetPrefab, new Vector3(target.x, 0, target.y), Quaternion.identity);

        List<Vector2Int> path = FindPathDFS(start, target);

        if (path != null)
        {
            List<Vector3> pathPositions = new List<Vector3>();

            foreach (var cell in path)
            {
                Instantiate(pathPrefab, new Vector3(cell.x, 0.1f, cell.y), Quaternion.identity);
                pathPositions.Add(new Vector3(cell.x, 0, cell.y));
            }

            GameObject player = Instantiate(playerPrefab, new Vector3(start.x, 0, start.y), Quaternion.identity);
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            playerMovement.SetPath(pathPositions);
        }
        else
        {
            Debug.Log("No path found");
        }
    }

    void DrawGrid()
    {
        for (int x = 0; x < _grid.Rows; x++)
        {
            for (int y = 0; y < _grid.Cols; y++)
            {
                if (_grid.IsPassable(x, y))
                {
                    Instantiate(groundPrefab, new Vector3(x, 0, y), Quaternion.identity);
                }
                else
                {
                    Instantiate(obstaclePrefab, new Vector3(x, 0, y), Quaternion.identity);
                }
            }
        }
    }

    List<Vector2Int> FindPathDFS(Vector2Int start, Vector2Int target)
    {
        Stack<Vector2Int> stack = new Stack<Vector2Int>();
        Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();
        bool[,] visited = new bool[_grid.Rows, _grid.Cols];

        stack.Push(start);
        visited[start.x, start.y] = true;

        while (stack.Count > 0)
        {
            Vector2Int current = stack.Pop();

            if (current == target)
            {
                return ReconstructPath(cameFrom, start, target);
            }

            foreach (var neighbor in GetNeighbors(current))
            {
                if (!visited[neighbor.x, neighbor.y] && _grid.IsPassable(neighbor.x, neighbor.y))
                {
                    stack.Push(neighbor);
                    visited[neighbor.x, neighbor.y] = true;
                    cameFrom[neighbor] = current;
                }
            }
        }

        return null; // No path found
    }

    List<Vector2Int> ReconstructPath(Dictionary<Vector2Int, Vector2Int> cameFrom, Vector2Int start, Vector2Int target)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Vector2Int current = target;

        while (current != start)
        {
            path.Add(current);
            current = cameFrom[current];
        }

        path.Add(start);
        path.Reverse();
        return path;
    }

    List<Vector2Int> GetNeighbors(Vector2Int cell)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();

        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(1, 0),  // Right
            new Vector2Int(-1, 0), // Left
            new Vector2Int(0, 1),  // Up
            new Vector2Int(0, -1)  // Down
        };

        foreach (var dir in directions)
        {
            Vector2Int neighbor = new Vector2Int(cell.x + dir.x, cell.y + dir.y);
            if (neighbor.x >= 0 && neighbor.x < _grid.Rows && neighbor.y >= 0 && neighbor.y < _grid.Cols)
            {
                neighbors.Add(neighbor);
            }
        }

        return neighbors;
    }
}
