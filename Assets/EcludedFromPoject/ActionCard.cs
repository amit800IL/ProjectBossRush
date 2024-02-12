using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionCard : MonoBehaviour
{
    [SerializeField] private CardDataSO cardData;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI numText;

    private void Start()
    {
        image.sprite = cardData.graphic;
        numText.text = cardData.cardPower.ToString();
    }

    public int GetCardPower()
    {
        return cardData.cardPower;
    }

    public CardType GetCardType()
    {
        return cardData.type;
    }
}
