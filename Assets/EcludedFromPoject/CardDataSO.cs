using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "ScriptableObject/CardDataScriptable")]
public class CardDataSO : ScriptableObject
{
    public Sprite graphic;
    public CardType type;
    public int cardPower;

}
public enum CardType
{
    Movement,
    Attack
}
