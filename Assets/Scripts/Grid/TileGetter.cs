using UnityEngine;

public static class TileGetter
{
    private static LayerMask tileMask = LayerMask.GetMask("Tile");


    //private static bool IsObjectInsideBounds(Vector3 boundPosition, Vector3 tileCenter)
    //{
    //    //float tileSizeX = 1f;
    //    //float tileSizeZ = 1f;

    //    //float minX = tileCenter.x - tileSizeX / 2;
    //    //float maxX = tileCenter.x + tileSizeX / 2;
    //    //float minZ = tileCenter.z - tileSizeZ / 2;
    //    //float maxZ = tileCenter.z + tileSizeZ / 2;

    //    //return (boundPosition.x >= minX && boundPosition.x <= maxX && boundPosition.z >= minZ && boundPosition.z <= maxZ);
    //}
    public static Tile GetTileFromCamera(Vector3 position, Camera camera, out RaycastHit raycastHit)
    {
        Ray ray = camera.ScreenPointToRay(position);

        bool raycast = Physics.Raycast(ray, out raycastHit, Mathf.Infinity, tileMask);

        if (raycast)
        {
            Tile tile = raycastHit.collider.GetComponent<Tile>();
            return tile;
        }

        return null;
    }
}
