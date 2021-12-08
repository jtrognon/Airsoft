using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    //grid variables
    public bool walkable;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;

    //cost variables
    public int gCost;
    public int hCost;

    public Node parent; //parent node. Is used to find the path to the start node

    //node class
    public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY){
        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int fCost {
        get {
            return gCost + hCost;
        }
    }
}
