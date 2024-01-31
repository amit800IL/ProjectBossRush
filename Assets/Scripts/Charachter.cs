using UnityEngine;

public class Charachter : MonoBehaviour
{
    [SerializeField] private Rigidbody2D charchterRigidBody;
    [SerializeField] private Vector2 moveRange;
    private float HP = 0.0f;

    public void MoveCharchterToPosition(Vector2 targetPositionInGrid)
    {
        Vector2 targetPosition = new Vector2(targetPositionInGrid.x, targetPositionInGrid.y);

        if (IsCharachterInMoveRange(targetPosition))
        {
            charchterRigidBody.position = targetPosition;
        }
    }
    private bool IsCharachterInMoveRange(Vector2 newPosition)
    {
        return Mathf.Abs(newPosition.x - transform.position.x) <= moveRange.x && Mathf.Abs(newPosition.y - transform.position.y) <= moveRange.y;
    }
}




