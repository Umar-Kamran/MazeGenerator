public class OuterWall
{
    #region Attributes
    /// <summary>
    /// Node this OuterWall is at
    /// </summary>
    public Node Node { get; private set; }

    /// <summary>
    /// Enum stating whether this outer wall is on the top, bottom, left or right of the node
    /// </summary>
    public enum Side
    {
        Up,
        Down,
        Left,
        Right
    }

    /// <summary>
    /// Stores what side of the node this wall is
    /// </summary>
    public Side WallSide { get; private set; }

    /// <summary>
    /// stores whether this is an entrance
    /// </summary>
    public bool Entrance { get; private set; }

    #endregion

    #region Constructor

    /// <summary>
    /// Creates an instance of an Outer wall
    /// </summary>
    /// <param name="node">node this outer wall is for</param>
    /// <param name="side">0 = up, 1 = down, 2 = left, 3 = right</param>
    public OuterWall(Node node, int side)
    {
        Node = node;
        WallSide = (Side)side;
    }

    /// <summary>
    /// Sets this outerwall as an entracnce
    /// </summary>
    public void SetEntrance() { Entrance = true; }
    #endregion

}
