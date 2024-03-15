using System.Collections.Generic;
using UnityEngine;

public static class TileGetter
{
    private static LayerMask tileMask = LayerMask.GetMask("Tile");
    public static Tile GetTile(Vector2 position, out RaycastHit2D raycastHit)
    {
        raycastHit = Physics2D.Raycast(position, Vector2.zero, Mathf.Infinity, tileMask);

        Tile tile = raycastHit.collider.GetComponent<Tile>();

        if (raycastHit && (tileMask.value & (1 << raycastHit.collider.gameObject.layer)) != 0)
            return tile;
        else
            return null;
    }

    public static List<Tile> GetListTiles(List<Tile> tiles, Vector2 vector, out RaycastHit2D raycastHit)
    {
        List<Tile> hitTiles = new List<Tile>();

        raycastHit = new RaycastHit2D();

        foreach (Tile tile in tiles)
        {
            raycastHit = Physics2D.Raycast(vector, Vector2.zero, Mathf.Infinity, tileMask);

            if (raycastHit && raycastHit.collider.GetComponent<Tile>() == tile)
            {
                hitTiles.Add(tile);
            }
        }

        return hitTiles;
    }
}
