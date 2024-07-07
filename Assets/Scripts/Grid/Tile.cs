using System;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2Int tilePosition { get; private set; }

    [field: SerializeField] public Transform OccupantContainer { get; private set; }
    [SerializeField] private TileType[] tileType;
    [SerializeField] private List<Effect> effectsOnTile = new();
    [SerializeField] GameObject AttackDecal;
    [SerializeField] GameObject DefendDecal;
    [SerializeField] GameObject EffectVisual;
    private Entity occupant;
    public bool IsTileOccupied => occupant != null;
    public void Initialize(int x, int y)
    {
        tilePosition = new Vector2Int(x, y);
    }


    public void SetTileType(TileType[] type)
    {
        tileType = type;
    }

    public bool IsTileOfType(TileType type)
    {
        foreach (TileType item in tileType)
        {
            if (item == type) return true;
        }
        return false;
    }

    public void OccupyTile(Entity ocuupantObject)
    {
        occupant = ocuupantObject;

        ocuupantObject.transform.position = OccupantContainer.position;

        ApplyEffectToOccupant();
    }

    public void ClearTile()
    {
        occupant = null;
    }

    public Entity GetOccupier()
    {
        return occupant;
    }

    public void RenderTactical(Hero hero)
    {
        AttackDecal.SetActive(hero.AttackPosCondition(this));
        DefendDecal.SetActive(hero.DefendPosCondition(this));
    }

    public void StopTactical()
    {
        AttackDecal.SetActive(false);
        DefendDecal.SetActive(false);
    }

    public void AddTileEffect(Effect effect)
    {
        for (int i = 0; i < effectsOnTile.Count; i++)
        {
            if (effectsOnTile[i].Type == effect.Type)
            {
                effectsOnTile[i] = new(effect);
                return;
            }
        }
        effectsOnTile.Add(effect);
        EffectVisual.SetActive(true);
    }

    public void UpdateEffects()
    {
        for (int i = 0; i < effectsOnTile.Count; i++)
        {
            if (effectsOnTile[i].duration > 1)
            {
                effectsOnTile[i] = effectsOnTile[i].LowerDuration();
            }
            else
            {
                effectsOnTile.RemoveAt(i);
                EffectVisual.SetActive(false);
                i--;
            }
        }
        ApplyEffectToOccupant();
    }

    private void ApplyEffectToOccupant()
    {
        foreach (Effect effect in effectsOnTile)
        {
            switch (effect.Type)
            {
                case EffectType.DamageOverTime:
                    if (IsTileOccupied)
                    {
                        Hero hero = (Hero)GetOccupier();
                        hero.TakeDamage(effect.amount);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}

public enum TileType
{
    CloseRange,
    MediumRange,
    LongRange,
    Flank,
}
