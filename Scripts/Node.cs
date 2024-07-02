using System;
using System.Collections.Generic;

public class Node
{
    #region Attributes
    /// <summary>
    /// X co-ordinate of this node
    /// </summary>
    public int X { get; private set; }
    /// <summary>
    /// y co-ordinate of this node
    /// </summary>
    public int Y { get; private set; }

    /// <summary>
    /// Stores whether the current node has been visited i.e. can be traveresed to
    /// </summary>
    public bool Visited { get; private set; }

    /// <summary>
    /// list of inner walls of this node
    /// </summary>
    public List<Wall> Walls { get; private set; } = new List<Wall>();

    /// <summary>
    /// list of outer walls of this node
    /// </summary>
    public List<OuterWall> OuterWalls { get; private set; } = new List<OuterWall>();
    #endregion

    #region Constructor
    /// <summary>
    /// Creates an instance of Node, specifiying its grid position
    /// </summary>
    /// <param name="x">X position</param>
    /// <param name="y">Y position</param>
    public Node(int x, int y)
    {
        X = x;
        Y = y;
        Visited = false;
    }
    #endregion

    #region Methods
    /// <summary>
    /// Logic for when node is visited. Sets visited to true and increments visited count of each wall
    /// </summary>
    /// <exception cref="Exception">Node already visited</exception>
    public void SetVisited()
    {
        if (Visited)
            throw new Exception("Node visited twice");

        Visited = true;
    }

    /// <summary>
    /// Adds a wall to the list of walls that divide this node
    /// </summary>
    /// <param name="wall">Wall to add to list</param>
    /// <exception cref="Exception">List of walls already larger than 4</exception>
    public void AddWall(Wall wall)
    {
        if (Walls.Count > 4)
            throw new Exception("Node contains more than 4 walls");

        Walls.Add(wall);
    }
    /// <summary>
    /// Adds an outer wall to this node if it is a border node
    /// </summary>
    /// <param name="wall">Wall to add to list</param>
    /// <exception cref="Exception">List of Outer walls already larger than 4 </exception>
    public void AddWall(OuterWall wall)
    {
        if (Walls.Count > 4)
            throw new Exception("Node contains more than 4 outer walls");

        OuterWalls.Add(wall);
    }
    /// <summary>
    /// Sets an Outer Wall of this node to an entrance
    /// </summary>
    /// <exception cref="Exception">Node has no outer walls</exception>
    public void RemoveOuterWall()
    {
        if (OuterWalls.Count < 1)
            throw new Exception("No Outer Walls to remove");

        OuterWalls[0].SetEntrance();
    }
    #endregion
}
