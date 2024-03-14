using UnityEngine;

public class DoNothing : EnemyAction
{
    public override void DoActionOnHero(Hero hero)
    {
        Debug.Log("I Have no strength in me");
    }
}
