
using UnityEngine;

public class LevelCreator : MonoBehaviour
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
                Instantiate(tileObject.TilePrefab, new Vector2(x, y), Quaternion.identity);
            }
        }
    }

}
