using UnityEngine;
using UnityEngine.InputSystem;

public static class TileGetter
{
    private static LayerMask tileMask = LayerMask.GetMask("Tile");

    public static Tile GetTileAtPosition(Vector3 position, out RaycastHit raycastHit)
    {
        Vector3 rayDirection = Vector3.down;

        Ray ray = new Ray(position, rayDirection);

        bool raycast = Physics.Raycast(ray, out raycastHit, Mathf.Infinity, tileMask);

        Debug.DrawRay(position, rayDirection, Color.red, 10f);

        Debug.Log("Tile has been hit : " + raycast);

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