using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.VFX;

public abstract class Hero : Entity
{
    public static event Action<Hero> OnHeroSpawned;
    public static event Action<Hero> OnHeroHealthChanged;
    public static event Action<Hero> OnHeroDefenceChanged;
    public static event Action<Hero> OnHeroDeath;
    public static event Action<Hero> OnHeroRevived;

    public static event Action<Hero> OnHeroWalk;
    public static event Action<Hero> OnHeroAttack;
    public static event Action<Hero> OnHeroDefend;
    public static event Action<Hero> OnHeroInjured;

    private static int lowHPThreshold = 40;
    public bool HeroIsAlive { get; private set; } = true;

    [field: Header("General Variables")]
    [field: SerializeField] public Animator heroAnimator { get; protected set; }

    [SerializeField] private Renderer heroSpriteRenderer;
    private Material material;
    private LocalKeyword lowHPKeyword;

    [SerializeField] private HeroSpriteChange spriteChange;
    public SymbolTable RewardSymbolTable { get; private set; } = new SymbolTable();
    [field: SerializeField] public SymbolUI RewardResourcesUI { get; private set; }

    [field: SerializeField] public GameObject[] RewardResources { get; private set; }
    [field: SerializeField] public ParticleSystem AttackingParticle { get; protected set; }
    [SerializeField] protected ParticleSystem defendingParticle;
    [field: SerializeField] public ParticleSystem SlashParticle { get; protected set; }
    public bool HasHeroUnlockedMovement { get; protected set; } = false;
    public bool IsHeroOnNewPosition { get; protected set; } = false;

    [field: Header("Symbol Table")]
    [field: SerializeField] public SymbolTable SymbolTable { get; protected set; } = new SymbolTable();

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

    [field: SerializeField] public VisualEffect attackVFX { get; protected set; }

    [SerializeField] protected VisualEffect FrontHealingVFX;
    [SerializeField] protected VisualEffect BackHealingVFX;

    [SerializeField] private float vfxTimer;

    [SerializeField] protected HeroThrowingWeapon heroThrowingWeapon;

    [field: SerializeField] public AudioSource[] AttackingAudio { get; protected set; }

    [field: SerializeField] public AudioSource[] HurtingAudio { get; protected set; }

    private void Awake()
    {
        material = heroSpriteRenderer.material;
        lowHPKeyword = new(material.shader, "_ISLOWHP");
    }

    protected virtual void Start()
    {
        HeroSpawn();
    }

    public void HeroSpawn()
    {
        HP = HeroData.maxHP;

        OnHeroSpawned?.Invoke(this);

        OnHeroHealthChanged?.Invoke(this);
        OnHeroDefenceChanged?.Invoke(this);

        if (attackVFX != null)
            attackVFX.Stop();

        if (FrontHealingVFX != null)
            FrontHealingVFX.Stop();

        if (BackHealingVFX != null)
            BackHealingVFX.Stop();


        movementAmount = HeroData.maxMovementAmount;
    }

    public void SoundAudio(AudioSource[] audioSources)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource == null) break;

            audioSource.Play();
        }
    }

    public void RestartHeroOnRevive()
    {
        if (!HeroIsAlive)
        {
            heroAnimator.SetTrigger("Revive");
            heroThrowingWeapon.gameObject.SetActive(false);
            HeroIsAlive = true;
            OnHeroRevived?.Invoke(this);
        }
    }

    public virtual IEnumerator ActivateAttackVfx()
    {
        if (attackVFX != null)
        {
            attackVFX.Play();
            heroAnimator.SetTrigger("Attack");
            yield return new WaitForSeconds(vfxTimer);
            attackVFX.Stop();
        }
    }

    protected virtual IEnumerator ActivateAttackVfx(Boss boss)
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
        if (movementAmount >= amountToReduce)
        {
            return true;
        }
        //else Debug.Log($"movement = {movementAmount}, amount to reduce = {amountToReduce}");
        return false;
        //return movementAmount > 0 && movementAmount >= amountToReduce;
    }

    public int GetHeroMovement() { return movementAmount; }

    public void HeroNewTurnRestart()
    {
        //movementAmount = 0;
    }

    public void ResetHeroMovement()
    {
        //movementAmount = 0;
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

        if (HP <= 0)
        {
            HP = 0;
            HeroIsAlive = false;
        }

        heroAnimator.SetTrigger("Injured");

        OnHeroHealthChanged?.Invoke(this);
        OnHeroDefenceChanged?.Invoke(this);
        OnHealthChange();

        OnHeroInjured?.Invoke(this);

        if (HP <= 0 && gameObject.activeSelf)
        {
            StartCoroutine(DeactivatePlayerTimer());
        }
    }

    private IEnumerator DeactivatePlayerTimer()
    {
        heroThrowingWeapon.gameObject.SetActive(true);
        heroAnimator.ResetTrigger("Revive");
        heroAnimator.SetTrigger("Death");
        yield return new WaitForSeconds(5f);
        OnHeroDeath?.Invoke(this);
        gameObject.SetActive(false);
    }

    public void GetHeal(int incHealth)
    {
        HP += incHealth;
        if (HP > HeroData.maxHP)
        {
            HP = HeroData.maxHP;
        }

        FrontHealingVFX.Play();
        BackHealingVFX.Play();
        OnHeroHealthChanged?.Invoke(this);
        OnHealthChange();
    }

    private void OnHealthChange()
    {
        if (HP != 0 && HP <= HeroData.maxHP / 100f * lowHPThreshold)
        {
            material.SetKeyword(lowHPKeyword, true);
        }
        else
        {
            material.SetKeyword(lowHPKeyword, false);
        }
    }

    public virtual bool HeroAttackBoss(Boss boss)
    {
        if (CanHeroAttack(boss))
        {
            AttackingParticle.Play();
            boss.TakeDamage(SymbolTable.GetDamage());
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



