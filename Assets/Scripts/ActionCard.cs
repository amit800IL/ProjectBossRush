using TMPro;
using UnityEngine;

public class ActionCard : MonoBehaviour
{
    [SerializeField] private CardDataSO cardData;
    [SerializeField] SpriteRenderer image;
    [SerializeField] TextMeshProUGUI numText;


    private void Start()
    {
        image.sprite = cardData.graphic;
        //numText.text = cardData.cardPower.ToString();
    }

    //public void Use()
    //{

    //}

    public int GetCardPower()
    {
        return cardData.cardPower;
    }

    public CardType GetCardType()
    {
        return cardData.type;
    }
}
