using System;

public class Grid
{
    #region Attributes
    /// <summary>
    /// 2-d Array storing all nodes
    /// </summary>
    public Node[,] Nodes { get; private set; }

    /// <summary>
    /// Array storing all inner walls
    /// </summary>
    public Wall[] Walls { get; private set; }

    /// <summary>
    /// Stores height of grid, i.e. number of rows
    /// </summary>
    public int Height { get; private set; }

    /// <summary>
    /// Stores width of grid, i.e. number of columns
    /// </summary>
    public int Width { get; private set; }

    /// <summary>
    /// Array storing all outer walls
    /// </summary>
    public OuterWall[] OuterWalls { get; private set; }

    private static Random rand = new Random();
    #endregion

    #region Constructor
    public Grid(int Height, int Width)
    {
        this.Height = Height;
        this.Width = Width;
        Nodes = new Node[Width, Height];


        //columns of walls + rows of walls (only inner walls)
        int columns = (Width - 1) * Height;
        int rows = Width * (Height - 1);
        Walls = new Wall[columns + rows];

        OuterWalls = new OuterWall[Height * 2 + Width * 2];
    }

    #endregion

    #region Methods

    /// <summary>
    /// Resets each node in the nodes array
    /// </summary>
    public void ResetNodes()
    {
        for (int i = 0; i < Width; i++)
            for (int j = 0; j < Height; j++)
                Nodes[i, j] = new Node(i, j);
    }

    /// <summary>
    /// Resets each wall in the Walls array
    /// </summary>
    public void ResetWalls()
    {
        int nextIndex = 0;
        //Creates vertical walls
        for (int i = 0; i < Width - 1; i++)
            for (int j = 0; j < Height; j++)
            {
                Node fromNode = Nodes[i, j];
                Node toNode = Nodes[i + 1, j];

                Walls[nextIndex] = new Wall(fromNode, toNode);

                fromNode.AddWall(Walls[nextIndex]);
                toNode.AddWall(Walls[nextIndex]);

                nextIndex++;
            }
        //Creates horizontal walls
        for (int i = 0; i < Width; i++)
            for (int j = 0; j < Height - 1; j++)
            {
                Node fromNode = Nodes[i, j];
                Node toNode = Nodes[i, j + 1];

                Walls[nextIndex] = new Wall(fromNode, toNode);

                fromNode.AddWall(Walls[nextIndex]);
                toNode.AddWall(Walls[nextIndex]);

                nextIndex++;
            }

        ResetOuterWalls();
        //Could combine both nested loops, however readability would suffer so left as is
    }

    /// <summary>
    /// Creates all Outer Walls and assigns them to their corresponding nodes
    /// </summary>
    private void ResetOuterWalls()
    {
        int nextIndex = 0;
        for (int i = 0; i < Width; i++)
        {
            Node node = Nodes[i, 0];
            OuterWalls[nextIndex] = new OuterWall(node, 0);
            node.AddWall(OuterWalls[nextIndex]);

            nextIndex++;

            node = Nodes[i, Height - 1];
            OuterWalls[nextIndex] = new OuterWall(node, 1);
            node.AddWall(OuterWalls[nextIndex]);
            nextIndex++;
        }

        for (int i = 0; i < Height; i++)
        {
            Node node = Nodes[0, i];
            OuterWalls[nextIndex] = new OuterWall(node, 2);
            node.AddWall(OuterWalls[nextIndex]);
            nextIndex++;

            node = Nodes[Width - 1, i];
            OuterWalls[nextIndex] = new OuterWall(node, 3);
            node.AddWall(OuterWalls[nextIndex]);
            nextIndex++;
        }
        CreateEntrance();
    }

    /// <summary>
    /// Sets two outer walls to be entrances randomly
    /// </summary>
    private void CreateEntrance()
    {
        int x1, x2;
        int y1, y2;

        x1 = rand.Next(0, Width / 2);
        y1 = rand.Next(0, Height / 2);
        // ensures this entrance is in the bottom left quadrant
        if (x1 <= y1)
            y1 = 0;
        else
            x1 = 0;

        x2 = rand.Next(Width / 2, Width);
        y2 = rand.Next(Height / 2, Height);
        // ensures this entrance is in the top right quadrant,
        // maximise difficulty whilst maintaing randomness
        if (x2 >= y2)
            y2 = Height - 1;
        else
            x2 = Width - 1;

        Nodes[x1, y1].RemoveOuterWall();
        Nodes[x2, y2].RemoveOuterWall();


    }
    #endregion
}
