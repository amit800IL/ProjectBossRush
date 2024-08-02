using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageProjectile : HeroProjectile
{
    [SerializeField] private List<GameObject> objectsToTurnOff = new List<GameObject>();
    public override void MoveProjectile(Vector3 endingPosition)
    {
        foreach (GameObject obj in objectsToTurnOff)
        {
            obj.SetActive(true);
        }

        attackProjectile.transform.position = startingPosition.position;

        Vector3 goToPosition = endingPosition - startingPosition.position;

        rigidBody.velocity = goToPosition * speed;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boss"))
        {
            foreach (GameObject obj in objectsToTurnOff)
            { 
                obj.SetActive(false);
            }

            projectileImpact.transform.position = attackProjectile.transform.position;
            rigidBody.velocity = Vector3.zero;
            projectileImpact.Play();
        }
    }
}
