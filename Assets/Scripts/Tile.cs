using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Tile : MonoBehaviour
{
    public Vector2 tilePosition { get; private set; }
    [field: SerializeField] public GameObject TilePrefab { get; private set; }
    [SerializeField] private TileType tileType;
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
        Color[] colors = new Color[5] { Color.white, Color.yellow, Color.green, Color.cyan, Color.magenta };

        int randomColor = Random.Range(0, colors.Length);

        spriteRenderer.color = colors[randomColor];
    }
}

public enum TileType
{
    regular,
    fire,
}
