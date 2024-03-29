using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Tile tileObject;
    [SerializeField] private Vector2Int gridSize;
    public Tile[,] Tiles { get; private set; }

    private void Awake()
    {
        CreateGrid();
    }

    public void CreateGrid()
    {
        Tiles = new Tile[gridSize.x, gridSize.y];

        Quaternion initilazeAngle = Quaternion.Euler(-90, 0, 0);

        transform.rotation = initilazeAngle;

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector3 gridPosition = new Vector2(x, y);

                Tiles[x, y] = Instantiate(tileObject, transform.position + gridPosition, Quaternion.identity, transform);
                Tiles[x, y].SetTileType(CalculateTileType(gridPosition));
            }
        }

        Quaternion finalAngle = Quaternion.Euler(0, 0, 0);

        transform.rotation = finalAngle;
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