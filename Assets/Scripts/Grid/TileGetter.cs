using UnityEngine;

public static class TileGetter
{
    private static LayerMask tileMask = LayerMask.GetMask("Tile");

    public static Tile GetTileAtPosition(Vector3 position, out RaycastHit raycastHit)
    {
        Vector3 rayDirection = Vector3.down;

        Ray ray = new Ray(position, rayDirection);

        bool raycast = Physics.Raycast(ray, out raycastHit, Mathf.Infinity, tileMask);

        Debug.DrawRay(position, rayDirection, Color.red, 10f);

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
        Vector3 rayDirection = Quaternion.Euler(15, 0, 0) * Vector3.forward;

        Ray ray = new Ray(position, rayDirection);

        bool raycast = Physics.Raycast(ray, out raycastHit, Mathf.Infinity, tileMask);

        Debug.DrawRay(position, rayDirection * 10, Color.red, 5f);

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