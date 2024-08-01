using System.Collections;
using UnityEngine;

public class BerzekerProjectile : MonoBehaviour
{
    [SerializeField] private GameObject slashProjectile;
    [SerializeField] private GameObject slahProjectileImpact;
    [SerializeField] private Hero hero;
    [SerializeField] private Transform startingPosition;
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private float speed;

    private void Start()
    {
        slashProjectile.transform.position = startingPosition.position;
    }

    public IEnumerator MoveProjectile(Vector3 endingPosition)
    {
        slashProjectile.transform.position = startingPosition.position;

        slashProjectile.SetActive(true);

        Vector3 goToPosition = endingPosition - startingPosition.position;

        rigidBody.velocity = goToPosition * speed;

        yield return new WaitForSeconds(0.2f);

        slahProjectileImpact.transform.position = slashProjectile.transform.position;
        slahProjectileImpact.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        slahProjectileImpact.SetActive(false);
        slashProjectile.SetActive(false);
    }
}
