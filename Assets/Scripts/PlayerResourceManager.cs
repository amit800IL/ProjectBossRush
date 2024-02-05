using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResourceManager : MonoBehaviour
{
    [SerializeField] int movement;
    [SerializeField] int attack;


    public void UseActionCard(ActionCard card)
    {
        if (card.GetCardType() == CardType.Movement)
        {
            ChangeMovementAmount(card.GetCardPower());
        }
        else if (card.GetCardType() == CardType.Attack)
        {
            ChangeAttackAmount(card.GetCardPower());
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
}
