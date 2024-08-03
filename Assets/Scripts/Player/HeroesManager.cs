using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HeroesManager : MonoBehaviour
{
    public static event Action OnHeroesDeath;

    private int heroesCount = 0;

    [SerializeField] private PlayerResourceManager playerResourceManager;
    [SerializeField] private Boss boss;
    [field: SerializeField] public List<Hero> heroList { get; private set; } = new List<Hero>();

    [SerializeField] private float heroAttackDelay = 0f;

    private void Start()
    {
        InitializeHeroList();

        PlayerResourceManager.OnTechniqueUsed += ActivateComboEffects;
        TurnsManager.OnPlayerTurnStart += NextTurnHeroMethods;
        Hero.OnHeroDeath += OnAllHeroesDeath;
    }

    private void OnDestroy()
    {
        PlayerResourceManager.OnTechniqueUsed -= ActivateComboEffects;
        TurnsManager.OnPlayerTurnStart -= NextTurnHeroMethods;
        Hero.OnHeroDeath -= OnAllHeroesDeath;
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

                    heroesCount = heroList.Count;
                }
            }
        }
    }

    public Hero GetRandomHero()
    {
        return heroList[Random.Range(0, heroList.Count)];
    }

    private void NextTurnHeroMethods()
    {
        foreach (Hero hero in heroList)
        {
            hero.HeroNewTurnRestart();
            hero.ResetTempHP();
        }
    }

    public IEnumerator CommandAttack()
    {
        foreach (Hero hero in heroList)
        {
            if (hero.HeroAttackBoss(boss))
            {
                playerResourceManager.AddSymbols(hero.SymbolTable);

                if (hero.heroAnimator != null)
                    hero.heroAnimator.SetTrigger("Attack");

                yield return new WaitForSeconds(heroAttackDelay);
            }
        }
    }

    public void OnAllHeroesDeath(Hero hero)
    {
        foreach (Hero listHero in heroList)
        {
            if (listHero == hero)
            {
                heroesCount--;

                if (heroesCount == 0)
                {
                    OnHeroesDeath?.Invoke();
                }
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

                    foreach (Hero hero in heroList)
                    {
                        hero.heroAnimator.SetTrigger("Attack");
                        hero.AttackingParticle.Play();
                    }

                    boss.TakeDamage(effect.amount);

                    Debug.Log($"combo dealt {effect.amount} damage");
                    break;
                case EffectType.HealAll:
                    foreach (Hero hero in heroList)
                    {
                        hero.GetHeal(effect.amount);
                    }
                    break;
                case EffectType.HealTarget:
                    selectedHero.GetHeal(effect.amount);
                    break;
                case EffectType.BuffDefense1:

                    break;
            }
        }
    }

}
