using UnityEngine;
using UnityEngine.Rendering;

public class Tile : MonoBehaviour
{
    public Vector2 tilePosition { get; private set; }
    [field: SerializeField] public GameObject TilePrefab { get; private set; }
    [SerializeField] private TileType[] tileType;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] bool danger;
    SpriteRenderer material;
    LocalKeyword keyword;

    [SerializeField] private GameObject occupant;
    public bool IsTileOccupied => occupant != null;
    private void Start()
    {
        tilePosition = transform.position;
        SetTileRandomColors();

        material = GetComponent<SpriteRenderer>();
        var shader = spriteRenderer.material.shader;
        keyword = new(shader, "_WARNING2");
    }

    [ContextMenu("materialUpdate")]
    public void MaterialUpdate()
    {
        spriteRenderer.material.SetKeyword(keyword, danger);
    }

    private void SetTileRandomColors()
    {
        Color[] colors = new Color[5] { Color.gray, Color.yellow, Color.black, Color.cyan, Color.magenta };

        int randomColor = Random.Range(0, colors.Length);

        spriteRenderer.color = colors[randomColor];
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

    public void OccupyTile(GameObject ocuupantObject)
    {
        occupant = ocuupantObject;
    }

    public void ClearTile()
    {
        occupant = null;
    }

    public GameObject GetOccupier()
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
