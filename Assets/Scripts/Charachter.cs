using UnityEngine;

public class Charachter : MonoBehaviour
{
    private float HP = 0.0f;
    public void MoveCharchterToPosition(Vector2 targetPositionInGrid)
    {
        transform.position = targetPositionInGrid;
    }
    //private bool IsCharachterInMoveRange(Vector2 newPosition)
    //{
    //    return Mathf.Abs(newPosition.x - transform.position.x) <= moveRange.x && Mathf.Abs(newPosition.y - transform.position.y) <= moveRange.y;
    //}
}




