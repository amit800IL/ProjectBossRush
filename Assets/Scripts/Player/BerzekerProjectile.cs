using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerzekerProjectile : HeroProjectile
{
    [SerializeField] private float newWidth = 0f;
    [SerializeField] private float newX = 0f;
    public override void MoveProjectile(Vector3 endingPosition)
    {
        float Distance = Vector3.Distance(startingPosition.position, endingPosition);

        if (Distance >= 3.3f && Distance < 4)
        {
            trailRenderer.widthMultiplier = newWidth;
            trailRenderer.transform.position += new Vector3(newX, 0f, 0f);
        }
        else
        {
            trailRenderer.widthMultiplier = 1.5f;
        }

        attackProjectile.gameObject.SetActive(true);

        attackProjectile.transform.position = startingPosition.position;

        attackProjectile.GetComponent<ParticleSystem>().Play();

        Vector3 goToPosition = endingPosition - startingPosition.position;


        rigidBody.velocity = goToPosition * speed;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boss"))
        {
            attackProjectile.gameObject.SetActive(false);
            projectileImpact.transform.position = attackProjectile.transform.position;
            projectileImpact.Play();
        }
    }
}
