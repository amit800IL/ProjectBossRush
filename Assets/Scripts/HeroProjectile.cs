using UnityEngine;

public class HeroProjectile : MonoBehaviour
{
    [SerializeField] private ParticleSystem attackProjectile;
    [SerializeField] private ParticleSystem projectileImpact;
    [SerializeField] private Transform startingPosition;
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private float speed;
    private void Start()
    {
        attackProjectile.transform.position = startingPosition.position;
    }

    public void MoveProjectile(Vector3 endingPosition)
    {
        attackProjectile.gameObject.SetActive(true);

        attackProjectile.transform.position = startingPosition.position;

        attackProjectile.Play();
        
        Vector3 goToPosition = endingPosition - startingPosition.position;

        rigidBody.velocity = goToPosition * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boss"))
        {
            attackProjectile.gameObject.SetActive(false);
            projectileImpact.transform.position = attackProjectile.transform.position;
            projectileImpact.Play();
        }
    }
}
