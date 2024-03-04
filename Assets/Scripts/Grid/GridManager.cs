using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Tile tileObject;
    [SerializeField] private Vector2Int gridSize;
    public Tile[,] tilesGrid { get; private set; }

    private void Awake()
    {
        CreateGrid();
    }
    public void CreateGrid()
    {
        tilesGrid = new Tile[gridSize.x, gridSize.y];

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2 gridPosition = new Vector2(x, y);
                tilesGrid[x, y] = Instantiate(tileObject, gridPosition, Quaternion.identity);
            }
        }
    }

}
