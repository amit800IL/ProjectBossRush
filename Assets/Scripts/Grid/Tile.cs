using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2Int tilePosition { get; private set; }

    [field: SerializeField] public Transform OccupantContainer { get; private set; }
    [SerializeField] private TileType[] tileType;
    [SerializeField] GameObject AttackDecal;
    [SerializeField] GameObject DefendDecal;
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
}

public enum TileType
{
    CloseRange,
    MediumRange,
    LongRange,
    Flank,
}
