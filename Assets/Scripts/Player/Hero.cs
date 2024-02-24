using UnityEngine;

public abstract class Hero : MonoBehaviour
{
    [SerializeField] protected float HP = 0.0f;
    [SerializeField] protected float Damage = 0.0f;
    [SerializeField] protected float Defense = 0.0f;
    public void MoveHeroToPosition(Vector2 targetPositionInGrid)
    {
        if (IsHeroInMoveRange(targetPositionInGrid))
            transform.position = targetPositionInGrid;
    }

    private bool IsHeroInMoveRange(Vector2 newPosition)
    {
        return OnOneTileRange(newPosition) && !OnDiagonalDirection(newPosition);
    }

    private bool OnOneTileRange(Vector2 newPosition)
    {
        return Mathf.Abs(newPosition.x - transform.position.x) <= 1f && Mathf.Abs(newPosition.y - transform.position.y) <= 1f;
    }

    private bool OnDiagonalDirection(Vector2 newPosition)
    {
        return Mathf.Abs(newPosition.x - transform.position.x) == 1f && Mathf.Abs(newPosition.y - transform.position.y) == 1f;
    }

    public void HealthDown()
    {
        HP -= 10f;
        Debug.Log(HP);

        if (HP == 0)
        {
            HP = 100f;

            gameObject.SetActive(false);
        }
    }
}




