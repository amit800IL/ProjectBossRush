using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    List<ActionCard> cardsInHand  = new List<ActionCard>();
    [SerializeField] CardPile deck;
    [SerializeField] CardPile trash;

    public void DrawCard()
    {
        cardsInHand.Add(deck.GetRandomCard());
    }
}
