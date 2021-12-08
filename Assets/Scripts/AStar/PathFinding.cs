using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public Transform seeker, target;

    GridClass grid;

    private void Awake() {
        grid  = GetComponent<GridClass>();
    }

    private void Update() {
        FindPath(seeker.position, target.position);
    }

    //----------------------------------------------------------------
    //pathfinding algorithm

    //Find the shortest path between two nodes
    void FindPath(Vector3 startPos, Vector3 targetPos){
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        //lists for the open and closed sets
        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0) {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++){ //for every nodes in the open list check if the fCost is better
                if (openSet[i].fCost < currentNode.fCost || (openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)) {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode) { //instantiate the path from start to end node
                RetracePath(startNode, targetNode);
                return;
            }

            //modify the open set
            foreach (Node neighbour in grid.GetNeighbours(currentNode))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                    continue;

                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour); //calculate the new gCost depending on the currentNode
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)){ //check if a better gCost can be provided or if the neighbour node is not in the openSet
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour)) //add the neighbour node if it's not already in it
                        openSet.Add(neighbour);
                }  
            }
        }
    }

    //--------------------------------------------------------------------------------------------------------------------
    //Additional methods for the pathfinding algorithm

    void RetracePath(Node startNode, Node endNode){ //retarce the path from end to start node using the parent variable
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
             path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        grid.path = path;
    }

    //get the distance between 2 nodes
    int GetDistance(Node nodeA, Node nodeB){
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY){
            return 14*dstY + 10*(dstX - dstY); //14 is the cost of diagonals and 10 for left/rigth/up/down
        }
        return 14*dstX + 10*(dstY - dstX);
    }

    /*
        GetDistance explanation:

        if the y axis is lower than the x axis then we make (y) diagonals and (x-y) moves to the left/right.
        And we do the opposit if x is lower than y
    */
}
