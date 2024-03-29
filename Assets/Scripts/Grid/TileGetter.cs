using UnityEngine;
using UnityEngine.InputSystem;

public static class TileGetter
{
    private static LayerMask tileMask = LayerMask.GetMask("Tile");
    public static Tile GetTileAtPosition(Vector3 position, out RaycastHit raycastHit)
    {
        Ray ray = new Ray(position, position);

        bool raycast = Physics.Raycast(ray, out raycastHit, Mathf.Infinity, tileMask);

        Debug.Log(raycast);

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
    public static Tile GetTileFromCamera(Vector3 position, out RaycastHit raycastHit)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);

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