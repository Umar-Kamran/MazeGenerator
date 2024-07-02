using System;
using System.Collections.Generic;

public class MazeGenerator
{
    #region Attributes
    /// <summary>
    /// Stores a reference to the grid
    /// </summary>
    private Grid Grid;

    /// <summary>
    /// Creates an instace of the random class
    /// </summary>
    private Random rnd = new Random();

    /// <summary>
    /// 
    /// </summary>
    private List<Wall> frontier = new List<Wall>();

    private List<Wall> visited = new List<Wall>();
    #endregion

    #region Constructor
    /// <summary>
    /// Creates an instance of the MazeGenerator and stores the grid to generate for
    /// </summary>
    /// <param name="grid"></param>
    public MazeGenerator(Grid grid)
    {
        this.Grid = grid;
    }
    #endregion

    #region Methods

    /// <summary>
    /// Function reseting the grid and calling the algorithm to generate the maze walls
    /// </summary>
    public void Generate()
    {
        Grid.ResetNodes();
        Grid.ResetWalls();

        int i = rnd.Next(Grid.Width);
        int j = rnd.Next(Grid.Height);
        Node initNode = Grid.Nodes[i, j];
        initNode.SetVisited();

        foreach (Wall wall in initNode.Walls)
        {
            frontier.Add(wall);
        }

        GenerationAlgorithm();
    }

    /// <summary>
    /// Algorithm used to expand what walls are kept in the maze
    /// 
    /// </summary>
    /// <remarks>Recursive method:
    /// 1) select wall from frontier (initially frontier is the inner walls of a random node)
    /// 2) if wall has an unexplored node it divides, explore that node and add its unused walls to the frontier
    /// 3) repeat step 2 until frontier empty, i.e. all walls  visited </remarks>
    private void GenerationAlgorithm()
    {
        if (frontier.Count == 0)
            return;

        int wallIndex = rnd.Next(frontier.Count);

        Wall wall = frontier[wallIndex];
        frontier.RemoveAt(wallIndex);
        visited.Add(wall);

        //If only one of the nodes a wall divides has been explored, traverse to the unexplored node
        if (wall.NumberVisited == 1)
        {
            wall.SetPassage();
            Node unvisitedNode = wall.GetUnvisitedNode();
            unvisitedNode.SetVisited();

            Wall[] newWalls = unvisitedNode.Walls.ToArray();

            foreach (Wall wallToAdd in newWalls)
            {
                if (visited.Contains(wallToAdd))
                    continue;
                frontier.Add(wallToAdd);
            }
        }
        GenerationAlgorithm();
    }

    #endregion
}
