using UnityEngine;

public class Charachter : MonoBehaviour
{
    [SerializeField] private Rigidbody2D charchterRigidBody;
    [SerializeField] private float moveSpeed;
    private Vector2Int moveRange;
    private float HP = 0.0f;

    public void MoveCharchterToPosition(Vector2 targerPosition)
    {
        float maxDistanceDelta = 10f;
        charchterRigidBody.position = Vector2.MoveTowards(charchterRigidBody.position, targerPosition, maxDistanceDelta);
    }
}
