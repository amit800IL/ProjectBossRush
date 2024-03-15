using UnityEngine;
using UnityEngine.Rendering;

public class Tile : MonoBehaviour
{

    [SerializeField] private LayerMask tileMask;
    public Vector2 tilePosition { get; private set; }
    [field: SerializeField] public GameObject TilePrefab { get; private set; }
    [SerializeField] private TileType[] tileType;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] bool danger;
    SpriteRenderer material;
    LocalKeyword keyword;

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

    public Tile CheckForTile()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, Vector2.zero, Mathf.Infinity, tileMask);

        if (raycastHit)
            return this;
        else
            return null;
    }

    public bool IsTileOccupied(GameObject occupier)
    {
        if (occupier.gameObject.transform.position == gameObject.transform.position)
        {
            return true;
        }

        return false;
    }

}

public enum TileType
{
    CloseRange,
    MediumRange,
    LongRange,
    Flank,
}
