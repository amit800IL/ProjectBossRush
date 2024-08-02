using UnityEngine;

public class BerzekerProjectile : MonoBehaviour
{
    [SerializeField] private ParticleSystem slashProjectile;
    [SerializeField] private ParticleSystem slahProjectileImpact;
    [SerializeField] private Hero hero;
    [SerializeField] private Transform startingPosition;
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private float speed;
    private void Start()
    {
        slashProjectile.transform.position = startingPosition.position;
    }

    public void MoveProjectile(Vector3 endingPosition)
    {
        slashProjectile.gameObject.SetActive(true);

        slashProjectile.transform.position = startingPosition.position;

        slashProjectile.Play();

        Vector3 goToPosition = endingPosition - startingPosition.position;

        rigidBody.velocity = goToPosition * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boss"))
        {
            slashProjectile.gameObject.SetActive(false);
            slahProjectileImpact.transform.position = slashProjectile.transform.position;
            slahProjectileImpact.Play();
        }
    }
}
