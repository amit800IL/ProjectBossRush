using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [field: SerializeField] public GameObject TilePrefab { get; private set; }
    [SerializeField] private TileType tileType;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Vector2Int tilePosition;

    private void Start()
    {
        Color[] colors = new Color[5] {Color.white, Color.red, Color.green, Color.cyan, Color.magenta};

        int randomColor = Random.Range(0, colors.Length);

        spriteRenderer.color = colors[randomColor];
    }
}

public enum TileType
{
    regular,
    fire,
}
