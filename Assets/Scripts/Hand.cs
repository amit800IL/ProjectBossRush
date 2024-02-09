using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : CardPile
{
    //List<ActionCard> cardsInHand  = new List<ActionCard>();
    [SerializeField] CardPile deck;
    [SerializeField] CardPile trash;
    [SerializeField] ActionCard selectedCard;

    public void DrawCard()
    {
        cards.Add(deck.GetRandomCard());
    }

    public void DrawCard(ActionCard card)
    {
        cards.Add(card);
    }

    public void Discard()
    {
        trash.AddCard(selectedCard);
    }
}
