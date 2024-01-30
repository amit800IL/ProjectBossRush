using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [field: SerializeField] public GameObject TilePrefab { get; private set; }
    [SerializeField] private TileType tileType;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private List<Vector2Int> positions; 
}

public enum TileType
{
    regular,
    fire,
}
