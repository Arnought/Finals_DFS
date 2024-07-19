using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    private int _numVertices;
    private List<int>[] _adjList;

    public Graph(int numVertices)
    {
        _numVertices = numVertices;
        _adjList = new List<int>[numVertices];
        for (int i = 0; i < numVertices; i++)
        {
            _adjList[i] = new List<int>();
        }
    }

    public void AddEdge(int v, int w)
    {
        _adjList[v].Add(w); // Add w to v’s list.
    }

    public List<int>[] GetAdjList()
    {
        return _adjList;
    }
}
