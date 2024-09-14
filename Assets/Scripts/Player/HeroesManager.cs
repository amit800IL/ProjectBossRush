using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class HeroesManager : MonoBehaviour
{
    public static event Action OnHeroesDeath;

    private int heroesCount = 0;

    [SerializeField] private PlayerResourceManager playerResourceManager;
    [SerializeField] private Boss boss;
    [field: SerializeField] public List<Hero> heroList { get; private set; } = new List<Hero>();

    [SerializeField] private float heroAttackDelay = 0f;

    [SerializeField] private RectTransform rewardTarget;

    private void Start()
    {
        InitializeHeroList();

        PlayerResourceManager.OnTechniqueUsed += ActivateComboEffects;
        TurnsManager.OnPlayerTurnStart += NextTurnHeroMethods;
        Hero.OnHeroDeath += OnAllHeroesDeath;
        Hero.OnHeroRevived += OnHeroRevive;
    }

    private void OnDestroy()
    {
        PlayerResourceManager.OnTechniqueUsed -= ActivateComboEffects;
        TurnsManager.OnPlayerTurnStart -= NextTurnHeroMethods;
        Hero.OnHeroDeath -= OnAllHeroesDeath;
        Hero.OnHeroRevived -= OnHeroRevive;
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

    public IEnumerator CommandAttack(Button attackingButton)
    {

        attackingButton.interactable = false;

        foreach (Hero hero in heroList)
        {
            playerResourceManager.ClearRewardSymbolTable(hero);

            if (hero.HeroAttackBoss(boss) && hero.HeroIsAlive)
            {
                if (hero.heroAnimator != null)
                    hero.heroAnimator.SetTrigger("Attack");

                playerResourceManager.AddSymbols(hero.SymbolTable);
                playerResourceManager.AddRewardSymbols(hero, hero.SymbolTable);

                yield return new WaitForSeconds(heroAttackDelay);

                if (!boss.IsBossAlive)
                {
                    yield break;
                }

                yield return RewardWithResoruces(hero);
            }
        }

        attackingButton.interactable = true;
    }

    private IEnumerator RewardWithResoruces(Hero hero)
    {
        Vector3 initialPositon = hero.transform.position;

        hero.RewardResourcesUI.gameObject.transform.localPosition = initialPositon;

        hero.RewardResourcesUI.gameObject.SetActive(true);

        float maxTimer = 1f;
        float timer = 0f;

        while (timer < maxTimer)
        {
            timer += Time.deltaTime;

            float progress = timer / maxTimer;

            hero.RewardResourcesUI.gameObject.transform.localPosition = Vector3.Lerp(initialPositon, rewardTarget.transform.localPosition, progress);

            yield return null;
        }

        playerResourceManager.AddSymbolsToUI();
        hero.RewardResourcesUI.gameObject.SetActive(false);
        hero.transform.position = initialPositon;
        hero.RewardResourcesUI.gameObject.transform.localPosition = initialPositon;
    }
    public void OnAllHeroesDeath(Hero hero)
    {
        foreach (Hero listHero in heroList)
        {
            if (listHero == hero && !hero.HeroIsAlive)
            {
                heroesCount--;

                if (heroesCount == 0)
                {
                    OnHeroesDeath?.Invoke();
                }
            }
        }
    }

    public void OnHeroRevive(Hero hero)
    {
        if (hero.HeroIsAlive)
            heroesCount++;
    }

    public void CommandDefend()
    {
        foreach (Hero hero in heroList)
        {
            if (hero.Defend() && hero.HeroIsAlive)
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
