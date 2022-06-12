using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridClass : MonoBehaviour
{
    //layer variables
    public LayerMask unwalkableMask;   
    
    //node variables
    float nodeDiameter;
    public float nodeRadius;

    //grid variables
    int gridSizeX, gridSizeY;
    Node[,] grid;
    public Vector2 gridWorldSize;

    //----------------------------------------------------------------
    //Initialization of the grid

    private void Start() {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x/nodeDiameter); //number of node that can be fit in the grid
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y/nodeDiameter);

        CreateGrid();
    }

    void CreateGrid() {
        grid = new Node[gridSizeX, gridSizeY]; //instantiate the grid
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x/2 - Vector3.forward * gridWorldSize.y/2; //pos of the bottom left corner of the map

        for (int x=0; x<gridSizeX; x++){ //loop to create each cell in the grid
            for (int y=0; y<gridSizeY; y++){
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius); //store the world point of x and y axis
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask)); //check if walkable
                grid[x, y] = new Node(walkable, worldPoint, x, y); //add node to the grid
            }
        }
    }

    //----------------------------------------------------------------
    //Additional methods for the grid class
    
    //get all the neighbours of a node
    public List<Node> GetNeighbours(Node node){
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) //if x and y = 0 then the coordinates are the one of the node were are looking at. So it's not a neighbour
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if ((checkX >= 0 && checkX < gridSizeX) && (checkY >= 0 && checkY < gridSizeY)){ //check if checkX and y are in the grid
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition) { //return a node in the grid depending on a world point
        float percentX = (worldPosition.x / gridWorldSize.x) + 0.5f; //percentage of x axis(between 0-1) for the world point
        float percentY = (worldPosition.z / gridWorldSize.y) + 0.5f;
        percentX = Mathf.Clamp01(percentX); //clamp percentage between 0-1
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.FloorToInt((gridSizeX) * percentX); //multiply percentage by gridSize to have the x pos of the cell and -1 to stay in the bounds
        int y = Mathf.FloorToInt((gridSizeY) * percentY);

        return grid[x, y]; //return the cell
    }

    public List<Node> path;
    private void OnDrawGizmos() {
        //Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y)); //draw the unwakable layerMask area
        if (grid != null){
            foreach (Node n in grid)
            {
                //Gizmos.color = (n.walkable)?Color.white:Color.red; //color = red if unwalkable else color = white if walkable
                if (path != null)
                    if (path.Contains(n))
                        Gizmos.color = Color.black;
                //Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter)); //draw cell
            }
        }
    }
}
