using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCardPile : CardPile<ActionCard>
{
    [SerializeField] private ActionDeckDataSO deck;
    [SerializeField] private ActionCard ACPrefab;

    private void Start()
    {
        print("hi");
        cards = new List<int>();
        for (int i = 0; i < deck.Cards.Count; i++)
        {
            cards.Add(i);
        }
    }

    [ContextMenu("getrndcard")]
    void GetRandomAC()
    {
        if(cards.Count == 0) return;
        print($"Draw: {deck.Cards[GetRandom()]}");
    }

    [ContextMenu("draw")]
    void GetAC()
    {
        if(cards.Count == 0) return;
        print($"Draw: {deck.Cards[GetItem()]}");
    }

    [ContextMenu("shuffle")]
    void ShuffleAC()
    {
        Shuffle();
    }
}
