using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResourceManager : MonoBehaviour
{
    [SerializeField] private int movement;
    [SerializeField] private int attack;

    public void UseActionCard(ActionCard card)
    {
        if (card.GetCardType() == CardType.Movement && movement > 0)
        {
            int movementAmountToDecrease = -1;
            ChangeMovementAmount(movementAmountToDecrease);
            
        }
        else if (card.GetCardType() == CardType.Attack && attack > 0)
        {
            int attackAmountToDecrease = -1;
            ChangeAttackAmount(attackAmountToDecrease);
        }
    }

    public void ChangeMovementAmount(int x)
    {
        movement += x;
    }

    public void ChangeAttackAmount(int x)
    {
        attack += x;
    }

    public int GetMovementAmount()
    {
        return movement;
    }

    public int GetAttackAmount()
    {
        return attack;
    }
}
