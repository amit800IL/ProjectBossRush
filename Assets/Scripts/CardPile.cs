using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardPile<T> : MonoBehaviour where T : Card
{
    public static event Action OnPileSizeChanged;

    [SerializeField] protected List<int> cards;

    [SerializeField] private int amountToDraw;

    public int GetRandom()
    {
        int rnd = Random.Range(0, cards.Count);
        int itemToReturn = cards[rnd];
        cards.RemoveAt(rnd);
        return itemToReturn;
    }

    public int GetItem()
    {
        int cardToReturn = cards[0];
        cards.RemoveAt(0);
        return cardToReturn;
    }

    public void Shuffle()
    {
        int currentPileSize = cards.Count;
        List<int> tempList = new(currentPileSize);
        int rnd;
        for (int i = 0; i < currentPileSize; i++)
        {
            rnd = Random.Range(0, cards.Count);
            tempList.Add(cards[rnd]);
            cards.RemoveAt(rnd);
        }
        cards = tempList;
    }

    public void AddCard(int card)
    {
        cards.Add(card);
    }
}
