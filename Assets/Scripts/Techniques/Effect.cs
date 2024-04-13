using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Effect
{
    public EffectType Type;
    public int duration;
    public int amount;

    public Effect(Effect effect)
    {
        Type = effect.Type;
        duration = effect.duration;
        amount = effect.amount;
    }

    public Effect(Effect effect, int newDuration)
    {
        Type = effect.Type;
        duration = newDuration;
        amount = effect.amount;
    }

    public void LowerDuration()
    {
        this = new(this, duration - 1);
    }
}

public enum EffectType
{
    DamageBoss,
    HealAll,
    Revive,
    BuffDefense1,
}
