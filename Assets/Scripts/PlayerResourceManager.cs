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
            card.ChangeCardText(movement);
        }
        else if (card.GetCardType() == CardType.Attack && attack > 0)
        {
            int attackAmountToDecrease = -1;
            ChangeAttackAmount(attackAmountToDecrease);
            card.ChangeCardText(attack);
        }
    }

    private void ChangeMovementAmount(int x)
    {
        movement += x;
    }

    private void ChangeAttackAmount(int x)
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
