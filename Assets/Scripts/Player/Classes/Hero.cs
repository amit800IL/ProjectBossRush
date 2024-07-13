using System;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public abstract class Hero : Entity
{
    public static event Action<Hero> OnHeroSpawned;
    public static event Action<Hero> OnHeroHealthChanged;
    public static event Action<Hero> OnHeroDefenceChanged;
    public static event Action<Hero> OnHeroDeath;

    public static event Action<Hero> OnHeroWalk;
    public static event Action<Hero> OnHeroAttack;
    public static event Action<Hero> OnHeroDefend;
    public static event Action<Hero> OnHeroInjured;

    [field: Header("General Variables")]
    [field: SerializeField] public Animator heroAnimator { get; protected set; }
    [SerializeField] private HeroSpriteChange spriteChange;
    [field: SerializeField] public ParticleSystem AttackingParticle { get; protected set; }
    [SerializeField] protected ParticleSystem defendingParticle;

    [field: SerializeField] public ParticleSystem HealingEffect { get; protected set; }
    [field: SerializeField] public ParticleSystem SlashParticle { get; protected set; }
    public bool HasHeroUnlockedMovement { get; protected set; } = false;
    public bool IsHeroOnNewPosition { get; protected set; } = false;
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

    [SerializeField] protected VisualEffect attackVFX;
    [SerializeField] private float vfxTimer;

    protected virtual void Start()
    {
        HP = HeroData.maxHP;

        OnHeroSpawned?.Invoke(this);

        OnHeroHealthChanged?.Invoke(this);
        OnHeroDefenceChanged?.Invoke(this);

        if (attackVFX != null)
            attackVFX.Stop();
    }

    protected virtual IEnumerator ActivateAttackVfx()
    {
        if (attackVFX != null)
        {
            attackVFX.Play();
            yield return new WaitForSeconds(vfxTimer);
            attackVFX.Stop();
        }
    }

    public void MoveHeroToPosition(Tile targetTile)
    {
        IsHeroOnNewPosition = false;

        CurrentTile.ClearTile();

        CurrentTile = targetTile;

        transform.position = targetTile.OccupantContainer.position;
        heroAnimator.SetTrigger("Walk");
        OnHeroWalk?.Invoke(this);

        if (transform.position == targetTile.OccupantContainer.position && targetTile != null)
        {
            targetTile.OccupyTile(this);
            IsHeroOnNewPosition = true;
        }
    }

    public void UnlockHeroMovement()
    {
        if (movementAmount <= 0)
        {
            movementAmount += HeroData.maxMovementAmount;
            HasHeroUnlockedMovement = true;
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
        HasHeroUnlockedMovement = false;
    }

    public void ResetTempHP()
    {
        tempHP = 0;
        OnHeroDefenceChanged?.Invoke(this);
    }

    public void ApplyTargetMarker(GameObject marker, int size)
    {
        targetMarker = Instantiate(marker, transform);
        float newScale = ((size * 2 - 1) * 1.5f - 0.2f);
        targetMarker.transform.localScale = new(newScale, 1, newScale);
        //targetMarker.transform.localScale = ((size * 2 - 1) * 1.5f - 0.2f) * Vector3.one;
    }

    public void RemoveTargetMarker()
    {
        if (targetMarker != null)
        {
            Destroy(targetMarker);
        }
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
        OnHeroInjured?.Invoke(this);
        spriteChange.OnHpLow(HP);

        if (HP <= 0)
        {
            OnHeroDeath?.Invoke(this);
            gameObject.SetActive(false);
        }
    }

    public void GetHeal(int incHealth)
    {
        HP += incHealth;
        if (HP > HeroData.maxHP)
        {
            HP = HeroData.maxHP;
        }

        HealingEffect.Play();
        OnHeroHealthChanged?.Invoke(this);

        spriteChange.OnHpLow(HP);
    }

    public virtual bool HeroAttackBoss(Boss boss)
    {
        if (CanHeroAttack(boss))
        {
            AttackingParticle.Play();
            boss.TakeDamage(HeroData.damage);
            OnHeroAttack?.Invoke(this);
            StartCoroutine(ActivateAttackVfx());
            return true;
        }
        else
        {
            Debug.Log("Hero can't attack");
            return false;
        }
    }

    public bool Defend()
    {
        if (CanHeroDefend())
        {
            tempHP += HeroData.defense;
            OnHeroDefenceChanged?.Invoke(this);
            heroAnimator.SetTrigger("Defend");
            OnHeroDefend?.Invoke(this);
            defendingParticle.Play();
            return true;
        }
        return false;
    }
    public abstract bool CanHeroAttack(Boss boss);

    public abstract bool AttackPosCondition(Tile tile);

    public abstract bool CanHeroDefend();

    public abstract bool DefendPosCondition(Tile tile);

}




