using UnityEngine;

public abstract class HeroProjectile : MonoBehaviour
{
    [SerializeField] protected GameObject attackProjectile;
    [SerializeField] protected ParticleSystem projectileImpact;
    [SerializeField] protected TrailRenderer trailRenderer;
    [SerializeField] protected Transform startingPosition;
    [SerializeField] protected Rigidbody rigidBody;
    [SerializeField] protected float speed;
    protected virtual void Start()
    {
        attackProjectile.transform.position = startingPosition.position;
    }

    public abstract void MoveProjectile(Vector3 endingPosition);
    protected abstract void OnTriggerEnter(Collider other);
}
