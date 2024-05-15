using System.Collections.Generic;
using UnityEngine;

public class HeroesManager : MonoBehaviour
{
    [SerializeField] private PlayerResourceManager playerResourceManager;
    [SerializeField] private Boss boss;
    [SerializeField] public List<Hero> heroList { get; private set; } = new List<Hero>();

    private void Start()
    {
        InitializeHeroList();

        PlayerResourceManager.OnTechniqueUsed += ActivateComboEffects;
        TurnsManager.OnPlayerTurnStart += NextTurnHeroMethods;
    }

    private void OnDestroy()
    {
        PlayerResourceManager.OnTechniqueUsed -= ActivateComboEffects;
        TurnsManager.OnPlayerTurnStart -= NextTurnHeroMethods;
    }

    private void InitializeHeroList()
    {
        Tile[,] tiles = GridManager.Instance.Tiles;

        if (tiles != null)
        {
            foreach (Tile tile in tiles)
            {
                if (tile.IsTileOccupied)
                {
                    Hero hero = (Hero)tile.GetOccupier();

                    heroList.Add(hero);
                }
            }
        }
    }

    private void NextTurnHeroMethods()
    {
        foreach (Hero hero in heroList)
        {
            hero.HeroNewTurnRestart();
            hero.ResetTempHP();
        }
    }

    public void CommandAttack()
    {
        foreach (Hero hero in heroList)
        {
            if (hero.HeroAttackBoss(boss))
            {
                playerResourceManager.AddSymbols(hero.SymbolTable);

                if (hero.heroAnimator != null)
                    hero.heroAnimator.SetTrigger("Attack");
            }
        }
    }

    public void CommandDefend()
    {
        foreach (Hero hero in heroList)
        {
            if (hero.Defend())
            {
                playerResourceManager.AddSymbols(new((int)SymbolTable.Symbols.Defense));
                hero.heroAnimator.SetTrigger("Defend");
            }
        }
    }

    [ContextMenu("Combo")]
    public void ActivateComboEffects(Effect[] effects, Hero selectedHero)
    {
        foreach (Effect effect in effects)
        {
            switch (effect.Type)
            {
                case EffectType.DamageBoss:
                    boss.TakeDamage(effect.amount);
                    Debug.Log($"combo dealt {effect.amount} damage");
                    break;
                case EffectType.HealAll:
                    foreach (Hero hero in heroList)
                    {
                        hero.TakeDamage(-effect.amount);
                    }
                    break;
                case EffectType.BuffDefense1:

                    break;
            }
        }
    }

}
