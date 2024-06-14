using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }

    [SerializeField] private Tile tileObject;
    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private List<GridObjectToSpawn> gridObjectsToSpawn = new List<GridObjectToSpawn>();
    private List<GameObject> spawnedObjects = new List<GameObject>();
    public Tile[,] Tiles { get; private set; }

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {

            Destroy(gameObject);
        }

        CreateGrid();
        TurnsManager.OnRoundStart += RollTiles;
    }

    private void OnDestroy()
    {
        TurnsManager.OnRoundStart -= RollTiles;
    }

    public void CreateGrid()
    {
        Tiles = new Tile[gridSize.x, gridSize.y];

        float tileGap = 0.1f;

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector3 gridPosition = new Vector3((x * (tileObject.transform.localScale.x + tileGap)), 0, y * ((tileObject.transform.localScale.z + tileGap)) * 1.1f);

                Tiles[x, y] = Instantiate(tileObject, (transform.position + gridPosition + new Vector3(-4, 0.5f, 0)), Quaternion.identity, transform);
                Tiles[x, y].Initialize(x, y);
                Tiles[x, y].SetTileType(CalculateTileType(new Vector2(x, y)));
            }
        }

        SpawnObjectsOnGrid();
    }

    public void ClearGrid()
    {
        if (Tiles != null)
        {
            foreach (Tile tile in Tiles)
            {
                if (tile != null)
                {
                    DestroyImmediate(tile.gameObject);
                }
            }
        }

        foreach (GameObject obj in spawnedObjects)
        {
            if (obj != null)
            {
                DestroyImmediate(obj);
            }
        }
        spawnedObjects.Clear();
    }
    private void SpawnObjectsOnGrid()
    {
        foreach (GridObjectToSpawn gridObject in gridObjectsToSpawn)
        {
            Tile targetTile = Tiles[gridObject.SpawnPosition.x, gridObject.SpawnPosition.y];

            GameObject heroObject = Instantiate(gridObject.GridObjectToSpawnObject, targetTile.OccupantContainer.position, gridObject.GridObjectToSpawnObject.transform.rotation);

            Hero hero = heroObject.GetComponent<Hero>();

            targetTile.OccupyTile(hero);

            hero.CurrentTile = targetTile;

            spawnedObjects.Add(heroObject);
        }
    }

    private TileType[] CalculateTileType(Vector2 position)
    {
        Debug.Log("this method is only accurate for grids where y = 6");
        TileType[] types = new TileType[1];
        if (position.y == 0 || position.y == gridSize.y - 1)
        {
            types = new TileType[2];
            types[1] = TileType.Flank;
        }

        if (position.x > 3)
        {
            types[0] = TileType.CloseRange;
        }
        else if (position.x < 2)
        {
            types[0] = TileType.LongRange;
        }
        else
        {
            types[0] = TileType.MediumRange;
        }

        return types;
    }

    public void StartTacticalView(Hero hero)
    {
        if (hero == null)
        {
            StopTacticalView();
            return;
        }

        foreach (Tile tile in Tiles)
        {
            tile.RenderTactical(hero);
        }
    }

    public void StopTacticalView()
    {
        foreach (Tile tile in Tiles)
        {
            tile.StopTactical();
        }
    }

    private void RollTiles()
    {
        foreach (Tile tile in Tiles)
        {
            tile.UpdateEffects();
        }
    }
}

[System.Serializable]
public class GridObjectToSpawn
{
    [SerializeField] private GameObject gridObjectToSpawnObject;
    [SerializeField] private Vector2Int spawnPosition;
    public GameObject GridObjectToSpawnObject { get => gridObjectToSpawnObject; private set => gridObjectToSpawnObject = value; }
    public Vector2Int SpawnPosition { get => spawnPosition; private set => spawnPosition = value; }
}
