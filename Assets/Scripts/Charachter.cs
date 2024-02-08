using UnityEngine;

public class Charachter : MonoBehaviour
{
    private float HP = 0.0f;
    public void MoveCharchterToPosition(Vector2 targetPositionInGrid)
    {
        if (IsCharachterInMoveRange(targetPositionInGrid))
        {
            transform.position = targetPositionInGrid;
        }
    }

    private bool IsCharachterInMoveRange(Vector2 newPosition)
    {
        return Mathf.Abs(newPosition.x - transform.position.x) <= 1f && Mathf.Abs(newPosition.y - transform.position.y) <= 1f;
    }
}




