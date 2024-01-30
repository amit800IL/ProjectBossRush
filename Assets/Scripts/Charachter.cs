using UnityEngine;

public class Charachter : MonoBehaviour
{
    [SerializeField] private Rigidbody2D charchterRigidBody;
    [SerializeField] private float moveSpeed;
    private Vector2Int moveRange;
    private float HP = 0.0f;

    public void MoveCharchterToPosition(Vector2 targerPosition)
    {
        Vector2 Distance = targerPosition - (Vector2)transform.position;
        charchterRigidBody.velocity = Distance * moveSpeed;
    }
}
