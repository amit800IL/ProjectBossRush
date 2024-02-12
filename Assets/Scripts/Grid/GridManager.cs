using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Tile tileObject;
    [SerializeField] private Vector2Int gridSize;

    private void Start()
    {
        CreateGrid();
    }

    public void CreateGrid()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2 gridPosition = new Vector2(x, y) - new Vector2(1, 0);
                Instantiate(tileObject.TilePrefab, gridPosition, Quaternion.identity);
            }
        }
    }

}
