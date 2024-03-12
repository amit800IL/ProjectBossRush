
using UnityEngine;

public class TileMediator<T> where T : MonoBehaviour
{
    private LayerMask tileMask;

    private static TileMediator<T> instance;
    public static TileMediator<T> Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new TileMediator<T>();
            }
            return instance;
        }
    }


    public void SetObjectOnTile(T overlappingObject, out Collider2D overLappedPoint, out Tile currentTile)
    {
        tileMask = LayerMask.GetMask("Tile");

        overLappedPoint = Physics2D.OverlapPoint(overlappingObject.transform.position, tileMask);

        currentTile = overLappedPoint.GetComponent<Tile>();
    }
    public void SetObjectOnTile(T overlappingObject, LayerMask layerMask , out Collider2D overlap, Tile currentTile = null)
    {
        overlap = Physics2D.OverlapPoint(overlappingObject.transform.position, layerMask);

        currentTile = overlap.GetComponent<Tile>();
    }

    public void UseRayCastOnTile(T overlappingObject, out RaycastHit2D raycastHit, out Tile currentTile)
    {
        tileMask = LayerMask.GetMask("Tile");

        raycastHit = Physics2D.Raycast(overlappingObject.transform.position, Vector2.zero, Mathf.Infinity, tileMask);

        currentTile = raycastHit.collider.GetComponent<Tile>();
    }

}