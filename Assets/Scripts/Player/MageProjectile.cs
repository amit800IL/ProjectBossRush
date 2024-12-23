using System.Collections.Generic;
using UnityEngine;

public class MageProjectile : HeroProjectile
{
    [SerializeField] private List<GameObject> objectsToTurnOff = new List<GameObject>();
    [SerializeField] private Collider projectileCollider;
    public override void MoveProjectile(Vector3 endingPosition)
    {
        isHeroAttacking = true;
        projectileCollider.enabled = true;

        attackProjectile.transform.position = startingPosition.position;

        foreach (GameObject obj in objectsToTurnOff)
        {
            obj.SetActive(true);
        }

        Vector3 goToPosition = endingPosition - startingPosition.position;

        rigidBody.velocity = goToPosition * speed;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.CompareTag("Boss") || other.gameObject.CompareTag("Berzeker")) && isHeroAttacking)
        {
            foreach (GameObject obj in objectsToTurnOff)
            {
                obj.SetActive(false);
            }

            projectileImpact.transform.position = attackProjectile.transform.position;
            rigidBody.velocity = Vector3.zero;
            projectileImpact.gameObject.SetActive(true);
            projectileImpact.Play();

            isHeroAttacking = false;

            projectileCollider.enabled = false;
        }
    }
}
