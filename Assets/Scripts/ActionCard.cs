using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionCard : Card
{
    [SerializeField] public int ID;
    [SerializeField] private CardDataSO cardData;
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI numText;

    private void Start()
    {
        SetVisuals();
    }

    public void SetData(CardDataSO data)
    {
        cardData = data;
        SetVisuals();
    }

    public void SetVisuals()
    {
        image.sprite = cardData.graphic;
        numText.text = cardData.cardPower.ToString();
    }

    public void Use()
    {

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
