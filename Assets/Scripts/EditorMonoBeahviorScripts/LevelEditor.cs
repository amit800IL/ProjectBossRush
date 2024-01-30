using System.Collections.Generic;
using UnityEngine;

public class LevelEditor : MonoBehaviour
{
    public List<Tile> levelObjects = new List<Tile>();

    //private void OnDrawGizmos()
    //{
    //    foreach (var obj in levelObjects)
    //    {
    //        Gizmos.color = obj.Color;
    //        Gizmos.DrawCube(new Vector2(obj.Position.x, obj.Position.y), Vector2.one);
    //    }
    //}

    public void AddObject()
    {
        //Tile newTile = new Tile();
        //levelObjects.Add(newTile);

        //// Instantiate the GameObject associated with the Tile
        //GameObject tileGameObject = null;

        //newTile.AttachGameObject(tileGameObject);

        //tileGameObject = Instantiate(newTile.Prefab, (Vector2)newTile.Position, Quaternion.identity);
        //// Attach the GameObject to the Tile
    }


    public void RemoveObject(int index)
    {
        //levelObjects.RemoveAt(index);
    }
}