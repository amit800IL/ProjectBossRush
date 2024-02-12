using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPile : MonoBehaviour
{
    List<ActionCard> cards;

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

    public void AddCard(ActionCard card)
    {
        cards.Add(card);
    }
}
