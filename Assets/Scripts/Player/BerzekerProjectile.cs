using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerzekerProjectile : HeroProjectile
{
    public override void MoveProjectile(Vector3 endingPosition)
    {
        isHeroAttacking = true;

        attackProjectile.transform.position = startingPosition.position;

        attackProjectile.gameObject.SetActive(true);

        attackProjectile.GetComponent<ParticleSystem>().Play();

        Vector3 goToPosition = endingPosition - startingPosition.position;

        rigidBody.velocity = goToPosition * speed;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boss") && isHeroAttacking)
        {
            attackProjectile.gameObject.SetActive(false);
            projectileImpact.transform.position = attackProjectile.transform.position;
            projectileImpact.Play();
            isHeroAttacking = false;
        }
    }
}
