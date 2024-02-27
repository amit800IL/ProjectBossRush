using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DeckData", menuName = "ScriptableObject/ActionDeckDataScriptable")]

public class ActionDeckDataSO : ScriptableObject
{
    public List<CardDataSO> Cards;
}
