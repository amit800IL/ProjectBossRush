using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject tileObject { get; private set; }
    public Vector2Int gridSize { get; private set; }

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
                Instantiate(tileObject, new Vector3(x, 0, y), Quaternion.identity);
            }
        }
    }
}
