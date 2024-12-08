using System;

public class Wall
{
    #region Attributes
    /// <summary>
    /// First node this wall divides
    /// </summary>
    public Node Node1 { get; private set; }

    /// <summary>
    /// Second wall this wall divides
    /// </summary>
    public Node Node2 { get; private set; }

    /// <summary>
    /// Stores 0 if neither nodes this wall divides have been visited, 1 if 1 visited, 2 if both visited
    /// </summary>
    public int NumberVisited
    {
        get
        {
            int numberVisited = (Node1.Visited ? 1 : 0) + (Node2.Visited ? 1 : 0);
            return numberVisited;
        }
    }

    /// <summary>
    /// Is this a wall or a passage
    /// </summary>
    public bool IsPassage { get; private set; }
    #endregion

    #region Constructors 
    /// <summary>
    /// Creates an instance of a wall 
    /// </summary>
    /// <param name="node1">First node wall divides</param>
    /// <param name="node2">Second node wall divides</param>
    public Wall(Node node1, Node node2)
    {
        this.Node1 = node1;
        this.Node2 = node2;
    }
    #endregion

    #region Methods
    /// <summary>
    /// Returns true if the wall divides the inputted node
    /// </summary>
    /// <param name="node">Node to check</param>
    public bool Contains(Node node)
    {
        return Node1 == node || Node2 == node;
    }

    /// <summary>
    /// Returns true if the pair of nodes inputted are divided by the wall
    /// </summary>
    public bool Contains(Node node1, Node node2)
    {
        return (Node1 == node1 && Node2 == node2) || (Node1 == node2 && Node2 == node1);
    }

    /// <summary>
    /// Returns the unvisited node of this wall. Must check if wall has unvisited nodes before calling, otherwise exception thrown
    /// </summary>
    /// <returns>Unvisited node</returns>
    /// <exception cref="Exception">Both nodes have been visited</exception>
    public Node GetUnvisitedNode()
    {
        if (Node1.Visited == false)
            return Node1;
        else if (Node2.Visited == false)
            return Node2;
        else
            throw new Exception("No unvisited node");
    }

    /// <summary>
    /// Sets this wall to be traversable through, i.e. invisible
    /// </summary>
    public void SetPassage() { IsPassage = true; }
    #endregion
}
