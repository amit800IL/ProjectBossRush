using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPile : MonoBehaviour //can be a generic pile of abstract cards
{
    protected List<ActionCard> cards;

    private void Start()
    {
        cards = new List<ActionCard>();
    }

    public ActionCard GetRandomCard()
    {
        if (cards.Count > 0)
        {
            int rnd = Random.Range(0, cards.Count);
            ActionCard cardToReturn = cards[rnd];
            cards.RemoveAt(rnd);
            return cardToReturn;
        }
        else return null;
    }

    public ActionCard GetCard(ActionCard card)
    {
        ActionCard cardToReturn = card;
        cards.Remove(card); 
        return cardToReturn;
    }

    public void AddCard(ActionCard card)
    {
        cards.Add(card);
    }
}
