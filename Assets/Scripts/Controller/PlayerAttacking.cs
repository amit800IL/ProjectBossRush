using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacking : MonoBehaviour
{
    PlayerResourceManager manager;

    private void PlayerAttack()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity);
        print("shoot");
        if (hit.collider != null)
        {
            print("hit");
            manager.UseActionCard(hit.collider.GetComponent<ActionCard>());
        }
    }
}
