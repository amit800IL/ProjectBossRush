using TMPro;
using UnityEngine;

public class PlayerResourceManager : MonoBehaviour
{
    [SerializeField] private int movement;
    [SerializeField] private int attack;
    [SerializeField] private TextMeshProUGUI movementResourceText;

    public void UseActionCard(ActionCard card)
    {
        if (card.GetCardType() == CardType.Movement)
        {
            ChangeMovementAmount(card.GetCardPower());
            movementResourceText.text = movement.ToString();
        }
        else if (card.GetCardType() == CardType.Attack)
        {
            ChangeAttackAmount(card.GetCardPower());
        }
    }
    public void UseMovementResource()
    {
        int movementAmountToDecrease = -1;
        ChangeMovementAmount(movementAmountToDecrease);
        movementResourceText.text = movement.ToString();
    }

    public void UseAttackResource()
    {
        int attackAmountToDecrease = -1;
        ChangeAttackAmount(attackAmountToDecrease);
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
