using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : CardPile<ActionCard>
{
    //List<ActionCard> cardsInHand  = new List<ActionCard>();
    [SerializeField] private CardPile<ActionCard> deck;
    [SerializeField] private CardPile<ActionCard> trash;
    [SerializeField] private ActionCard selectedCard;

    public void DrawCard()
    {
        cards.Add(deck.GetRandom());
    }

    public void DrawCard(ActionCard card)
    {
        cards.Add(card.ID);
    }

    public void Discard()
    {
        trash.AddCard(selectedCard.ID);
    }
}
