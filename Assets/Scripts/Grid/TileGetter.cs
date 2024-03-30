using System;
using UnityEngine;
using UnityEngine.InputSystem;

public static class TileGetter
{
    private static LayerMask tileMask = LayerMask.GetMask("Tile");
    public static void OccupyTile(Tile tile, GameObject gameObject)
    {
        tile.OccupyTile(gameObject);
    }
    public static Tile GetTileFromCamera(Vector3 position, Camera camera , out RaycastHit raycastHit)
    {
        Ray ray = camera.ScreenPointToRay(position);

        bool raycast = Physics.Raycast(ray, out raycastHit, Mathf.Infinity, tileMask);

        if (raycast)
        {
            Tile tile = raycastHit.collider.GetComponent<Tile>();
            Debug.Log(tile.tilePosition);
            return tile;
        }
        else
        {
            return null;
        }
    }
}
