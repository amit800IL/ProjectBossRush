using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2Int tilePosition { get; private set; }

    [field: SerializeField] public Transform OccupantContainer { get; private set; }
    [field: SerializeField] public GameObject TilePrefab { get; private set; }
    [SerializeField] private TileType[] tileType;
    [SerializeField] private MeshRenderer spriteRenderer;
    [SerializeField] private Entity occupant;
    public bool IsTileOccupied => occupant != null;
    public void Initialize(int x, int y)
    {
        tilePosition = new Vector2Int(x, y);
    }


    public void SetTileType(TileType[] type)
    {
        tileType = type;
    }

    public bool IsTileOfType(TileType type)
    {
        foreach (TileType item in tileType)
        {
            if (item == type) return true;
        }
        return false;
    }

    public void OccupyTile(Entity ocuupantObject)
    {
        occupant = ocuupantObject;

        ocuupantObject.transform.position = OccupantContainer.position;
    }

    public void ClearTile()
    {
        occupant = null;
    }

    public Entity GetOccupier()
    {
        return occupant;
    }
}

public enum TileType
{
    CloseRange,
    MediumRange,
    LongRange,
    Flank,
}
