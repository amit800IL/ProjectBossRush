using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Tile tileObject;
    [SerializeField] private Vector2Int gridSize;
    public Tile[,] tilesGrid { get; private set; }
    public Tile[,] Tiles { get; private set; }

    private void Awake()
    {
        CreateGrid();
    }

    public void CreateGrid()
    {
        tilesGrid = new Tile[gridSize.x, gridSize.y];
        Tiles = new Tile[gridSize.x, gridSize.y];

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2 gridPosition = new Vector2(x, y);
                tilesGrid[x, y] = Instantiate(tileObject, gridPosition, Quaternion.identity);
                Tiles[x, y] = Instantiate(tileObject, gridPosition, Quaternion.identity, transform);
                Tiles[x, y].SetTileType(CalculateTileType(gridPosition));
            }
        }
    }

    private TileType[] CalculateTileType(Vector2 position)
    {
        Debug.Log("this method is only accurate for grids where y = 6");
        TileType[] types = new TileType[1];
        if (position.x == 0 || position.x == gridSize.x - 1)
        {
            types = new TileType[2];
            types[1] = TileType.Flank;
        }

        if (position.y > 3)
        {
            types[0] = TileType.CloseRange;
        }
        else if (position.y < 2)
        {
            types[0] = TileType.LongRange;
        }
        else
        {
            types[0] = TileType.MediumRange;
        }

        return types;
    }
}