using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tile : MonoBehaviour
{
    public Vector2Int tilePosition { get; private set; }

    [field: SerializeField] public Transform OccupantContainer { get; private set; }
    [SerializeField] private TileType[] tileType;
    [SerializeField] private List<Effect> effectsOnTile = new();
    [SerializeField] GameObject InRangeDecal;
    [SerializeField] GameObject AttackDecal;
    [SerializeField] GameObject DefendDecal;
    [SerializeField] GameObject EffectVisual;
    [SerializeField] GameObject EffectVFX;
    [SerializeField] TextMeshPro EffectCounterText;
    private Entity occupant;
    public bool IsTileOccupied => occupant != null;

    private void Start()
    {
        UpdateEffectsCounterText();
    }

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

    //true if other tile shares 1 edge with this tile
    public bool IsTileNeighboring(Tile otherTile)
    {
        Vector2Int otherPos = otherTile.tilePosition;
        return (tilePosition.x == otherPos.x && Mathf.Abs(tilePosition.y - otherPos.y) == 1 ||
            tilePosition.y == otherPos.y && Mathf.Abs(tilePosition.x - otherPos.x) == 1);
    }

    public void OccupyTile(Entity ocuupantObject)
    {
        occupant = ocuupantObject;

        ocuupantObject.transform.position = OccupantContainer.position;

        ApplyEffectsToOccupant();
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

    public void HighlightIfInRange(Tile otherTile, int range)
    {
        Vector2Int otherPos = otherTile.tilePosition;
        if (Mathf.Abs(tilePosition.y - otherPos.y) + Mathf.Abs(tilePosition.x - otherPos.x) <= range)
        {
            InRangeDecal.SetActive(true);
        }
        else
        {
            InRangeDecal.SetActive(false);

        }
    }

    public void RemoveHighlight()
    {
        InRangeDecal.SetActive(false);
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
        EffectVFX.SetActive(true);
    }

    //called on every tile
    public void UpdateEffects()
    {
        if (effectsOnTile.Count > 0)
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
            ApplyEffectsToOccupant();
        }
        UpdateEffectsCounterText();
    }

    private void UpdateEffectsCounterText()
    {
        if (effectsOnTile.Count > 0)
        {
            EffectCounterText.text = effectsOnTile[0].duration.ToString();
        }
        else
        {
            EffectCounterText.text = string.Empty;
        }
    }

    private void ApplyEffectsToOccupant()
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
