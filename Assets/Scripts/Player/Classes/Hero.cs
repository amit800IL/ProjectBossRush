using System;
using UnityEngine;

public abstract class Hero : Entity
{
    public static event Action<Hero> OnHeroSpawned;
    public static event Action<Hero> OnHeroHealthChanged;
    public static event Action<Hero> OnHeroDefenceChanged;

    [field: Header("General Variables")]
    [field: SerializeField] public Animator heroAnimator { get; protected set; }
    [SerializeField] private HeroSpriteChange spriteChange;
    [SerializeField] protected ParticleSystem attackingParticle;
    [SerializeField] protected ParticleSystem defendingParticle;
    [field: SerializeField] public ParticleSystem SlashParticle { get; protected set; }
    public bool CanHeroMoved { get; protected set; } = false;
    public SymbolTable SymbolTable { get; protected set; }

    protected int movementAmount = 0;

    private GameObject targetMarker;

    [field: Header("Hero Attributes")]

    [field: SerializeField] public HeroDataSO HeroData { get; protected set; }
    [field: SerializeField] public int HP { get; protected set; }
    [SerializeField] public int tempHP { get; protected set; } = 0;

    [field: Header("Tile and raycast")]

    protected Tile currentTile;
    public Tile CurrentTile { get => currentTile; set => currentTile = value; }

    protected RaycastHit raycastHit;

    protected virtual void Start()
    {
        HP = HeroData.maxHP;

        OnHeroSpawned?.Invoke(this);

        OnHeroHealthChanged?.Invoke(this);
        OnHeroDefenceChanged?.Invoke(this);
    }

    public void MoveHeroToPosition(Tile targetTile)
    {
        CurrentTile.ClearTile();

        CurrentTile = targetTile;

        transform.position = targetTile.OccupantContainer.position;
        heroAnimator.SetTrigger("Walk");

        if (transform.position == targetTile.OccupantContainer.position && targetTile != null)
        {
            targetTile.OccupyTile(this);
        }
    }

    public void UnlockHeroMovement()
    {
        if (movementAmount <= 0)
        {
            movementAmount += HeroData.maxMovementAmount;
            CanHeroMoved = true;
        }
    }
    public void HeroMovemetAmountReduction(int amountToReduce)
    {
        movementAmount -= amountToReduce;
    }

    public bool CanHeroMove(int amountToReduce)
    {
        return movementAmount > 0 && movementAmount >= amountToReduce;
    }

    public void HeroNewTurnRestart()
    {
        movementAmount = 0;
    }

    public void ResetHeroMovement()
    {
        movementAmount = 0;
        CanHeroMoved = false;
    }

    public void ResetTempHP()
    {
        tempHP = 0;
        OnHeroDefenceChanged?.Invoke(this);
    }

    public void ApplyTargetMarker(GameObject marker, int size)
    {
        targetMarker = Instantiate(marker, transform);
        targetMarker.transform.localScale = ((size * 2 - 1) * 1.5f - 0.2f) * Vector3.one;
        print("hi");
    }

    public void TakeDamage(int incDmg)
    {
        if (incDmg <= tempHP)
        {
            tempHP -= incDmg;
        }
        else
        {
            incDmg -= tempHP;
            tempHP = 0;
            HP -= incDmg;
        }

        OnHeroHealthChanged?.Invoke(this);
        OnHeroDefenceChanged?.Invoke(this);

        heroAnimator.SetTrigger("Injured");
        spriteChange.OnHpLow(HP);

        if (HP <= 0)
        {
            gameObject.SetActive(false);
        }
        if (targetMarker != null)
        {
            Destroy(targetMarker);
        }
    }

    public void GetHeal(int incHealth)
    {
        HP += incHealth;
        if (HP > HeroData.maxHP)
        {
            HP = HeroData.maxHP;
        }

        OnHeroHealthChanged?.Invoke(this);

        spriteChange.OnHpLow(HP);
    }

    public abstract bool HeroAttackBoss(Boss boss);

    public bool Defend()
    {
        if (CanHeroDefend())
        {
            tempHP += HeroData.defense;
            OnHeroDefenceChanged?.Invoke(this);
            heroAnimator.SetTrigger("Defend");
            defendingParticle.Play();
            return true;
        }
        return false;
    }
    public abstract bool CanHeroAttack();

    public abstract bool AttackPosCondition(Tile tile);

    public abstract bool CanHeroDefend();

    public abstract bool DefendPosCondition(Tile tile);

}




