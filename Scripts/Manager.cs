using System.Collections.Generic;
using UnityEngine;
public class Manager : MonoBehaviour
{
    #region Attributes
    /// <summary>
    /// Stores the ui element for a node (only required if debugging)
    /// </summary>
    [SerializeField]
    private GameObject nodePrefab;

    /// <summary>
    /// stores the ui element for a wall
    /// </summary>
    [SerializeField]
    private GameObject wallPrefab;

    /// <summary>
    /// stores height and width of the grid
    /// </summary>
    private static readonly int Height = 21;
    private static readonly int Width = 21;

    /// <summary>
    /// Grid object
    /// </summary>
    Grid grid = new Grid(Height, Width);

    /// <summary>
    /// instance of MazeGenerator
    /// </summary>
    MazeGenerator mazeGenerator;

    /// <summary>
    /// Scale of ui elements horizontally
    /// </summary>
    float ScaleX;

    /// <summary>
    /// Scale of ui elements vertically
    /// </summary>
    float ScaleY;

    /// <summary>
    /// list of created ui walls
    /// </summary>
    private List<GameObject> InstantiatedWalls = new List<GameObject>();

    #endregion

    #region Methods

    void Start()
    {
        // formula to generate scaling factors
        ScaleX = 31f * 3 / (4f * Width);
        ScaleY = 31f * 3 / (4f * Height);

        Reset();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
    }

    /// <summary>
    /// Scales and translates x co-ordinate to scale with maze size
    /// </summary>
    /// <param name="x">x co-ordinate to scale</param>
    /// <returns>scaled x co-ordinate</returns>
    private float ConvertX(float x)
    {
        return (x - grid.Width / 2) * ScaleX;
    }

    /// <summary>
    /// Scales and translates y co-ordinate to scale with maze size
    /// </summary>
    /// <param name="y">y co-ordinate to scale</param>
    /// <returns>scaled y co-ordinate</returns>
    private float ConvertY(float y)
    {
        return (y - grid.Height / 2) * ScaleY;
    }

    /// <summary>
    /// Function to display nodes. Helpful for debugging and visualising so left implemented but not utilised.
    /// </summary>
    private void ViewNodes()
    {
        grid.ResetNodes();
        foreach (Node node in grid.Nodes)
        {
            GameObject temp = Instantiate(nodePrefab, new Vector2(ConvertX(node.X), ConvertY(node.Y)), Quaternion.identity);
            temp.transform.localScale = new Vector3(temp.transform.localScale.x * ScaleX, temp.transform.localScale.y * ScaleY, 1);
        }
    }


    /// <summary>
    /// Resets the UI elements and calls the maze generation algorithm to create a new maze. Then creates the ui elements for walls, 
    /// translates to their correct position, rotates if its a vertical wall, and scales them. this is done for the inner walls and then the outer walls
    /// </summary>
    public void Reset()
    {
        if (InstantiatedWalls.Count > 0)
            foreach (GameObject temp in InstantiatedWalls)
                Destroy(temp);

        mazeGenerator = new MazeGenerator(grid);
        mazeGenerator.Generate();

        foreach (Wall wall in grid.Walls)
        {
            if (wall.IsPassage == true) continue;
            Node node1 = wall.Node1;
            Node node2 = wall.Node2;

            Vector3 avgPos = new Vector3(ConvertX((node1.X + node2.X) / 2f), ConvertY((node1.Y + node2.Y) / 2f));
            GameObject temp = Instantiate(wallPrefab, avgPos, Quaternion.identity);
            InstantiatedWalls.Add(temp);

            if (node1.Y != node2.Y)
                temp.transform.Rotate(0, 0, 90f);

            temp.transform.localScale = new Vector3(temp.transform.localScale.x * ScaleX, temp.transform.localScale.y * ScaleY, 1);
        }

        foreach (OuterWall wall in grid.OuterWalls)
        {
            if (wall.Entrance != true)
            {
                Node node = wall.Node;
                int side = (int)wall.WallSide; // 0 = up, 1 = down, 2 = left, 3 = right
                bool horizontal = true;

                Vector2 Shift;
                if (side > 1)
                {
                    Shift = new Vector2(side == 2 ? -0.5f : 0.5f, 0);
                    horizontal = false;
                }
                else
                    Shift = new Vector2(0, side == 0 ? -0.5f : 0.5f);

                Vector3 pos = new Vector3(ConvertX(node.X + Shift.x), ConvertY(node.Y + Shift.y));
                GameObject temp = Instantiate(wallPrefab, pos, Quaternion.identity);
                InstantiatedWalls.Add(temp);

                temp.transform.localScale = new Vector3(temp.transform.localScale.x * ScaleX, temp.transform.localScale.y * ScaleY, 1);

                if (horizontal)
                    temp.transform.Rotate(0, 0, 90f);
            }
        }
    }
    #endregion
}
