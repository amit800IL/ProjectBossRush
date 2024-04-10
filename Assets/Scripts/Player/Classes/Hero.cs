using System;
using TMPro;
using UnityEngine;

public abstract class Hero : Entity
{
    [field: Header("General Variables")]
    [SerializeField] protected PlayerResourceManager playerResourceManager;
    [field: SerializeField] public Animator heroAnimator { get; protected set; }

    [SerializeField] protected ParticleSystem attackingParticle;
    [SerializeField] protected ParticleSystem defendingParticle;
    public bool CanHeroMoved { get; protected set; } = false;
    public SymbolTable SymbolTable { get; protected set; }

    protected int maxMovementAmount = 0;
    protected int movementAmount = 0;

    [Header("Hero Attributes")]

    [SerializeField] protected float HP = 0.0f;

    [SerializeField] protected float damage = 0.0f;

    [SerializeField] protected float Defense = 0.0f;

    [field: Header("Tile and raycast")]

    protected Tile currentTile;
    public Tile CurrentTile { get => currentTile; set => currentTile = value; }

    protected RaycastHit raycastHit;

    protected void Awake()
    {    
        TurnsManager.OnPlayerTurnStart += HeroNewTurnRestart;
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
            movementAmount += maxMovementAmount;
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

    public void TakeDamage(int incDmg)
    {
        HP -= incDmg;
        Debug.Log("Hero " + name + " has been attacked" + ", Health : " + HP);

        heroAnimator.SetTrigger("Injured");

        if (HP <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public abstract bool HeroAttackBoss(Boss boss);

    public abstract void HeroDefend(Boss boss);
    public abstract bool CanHeroAttack();
}




