using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Effect
{
    public EffectType Type;
    public int duration;
    public int amount;
}

public enum EffectType
{
    DamageBoss,
    HealAll,
    Revive,
    BuffDefense1,
}
