using UnityEngine;

public static class TileGetter
{
    private static LayerMask tileMask = LayerMask.GetMask("Tile");
    public static Tile GetTile(Vector2 vector, out RaycastHit2D raycastHit)
    {
        raycastHit = Physics2D.Raycast(vector, Vector2.zero, Mathf.Infinity, tileMask);

        Tile tile = raycastHit.collider.GetComponent<Tile>();

        if (raycastHit && (tileMask.value & (1 << raycastHit.collider.gameObject.layer)) != 0)
            return tile;
        else
            return null;
    }
}
