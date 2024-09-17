using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class HeroesManager : MonoBehaviour
{
    public static event Action OnHeroesDeath;

    private int heroesCount = 0;

    [SerializeField] private Camera mainCamera;

    [SerializeField] private PlayerResourceManager playerResourceManager;
    [SerializeField] private Boss boss;
    [field: SerializeField] public List<Hero> heroList { get; private set; } = new List<Hero>();

    [SerializeField] private float heroAttackDelay = 0f;

    [SerializeField] private RectTransform[] rewardTargets;

    [SerializeField] private Image[] fillingImage;

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
        Vector3 worldPosition = hero.transform.position + new Vector3(0, 2, 0);

        Vector3 screenPosition = mainCamera.WorldToScreenPoint(worldPosition);

        foreach (GameObject rewardItem in hero.RewardResources)
        {
            rewardItem.transform.position = screenPosition;

            string rewardText = rewardItem.GetComponentInChildren<TextMeshProUGUI>().text;

            if (rewardText == "0")
            {
                rewardItem.SetActive(false);
            }
            else
            {
                rewardItem.SetActive(true);
            }
        }

        foreach (Image image in fillingImage)
        {
            image.fillAmount = 0;
        }

        yield return new WaitForSeconds(0.2f);

        yield return MoveSymbolToTarget(hero, screenPosition);

        yield return GiveSymbolReward(hero, screenPosition);

        playerResourceManager.AddSymbolsToUI();

        foreach (GameObject rewardItem in hero.RewardResources)
        {
            rewardItem.transform.position = screenPosition;
            rewardItem.SetActive(false);
        }

        foreach (Image image in fillingImage)
        {
            image.fillAmount = 0;
        }
    }

    private IEnumerator MoveSymbolToTarget(Hero hero, Vector3 screenPosition, float maxTimer = 0.8f, float duration = 0f)
    {
        while (duration < maxTimer)
        {
            duration += Time.deltaTime;

            float progress = duration / maxTimer;

            float cosX = Mathf.Cos(Time.time * 2f);
            float cosY = Mathf.Cos(Time.time * 2f);

            Vector3 cosScreenPos = new Vector3(screenPosition.x * Random.Range(cosX, cosY), screenPosition.y * Random.Range(cosY, cosX), 0);

            Vector3 lerpedScreenPos = Vector3.Lerp(screenPosition, cosScreenPos, progress);

            Vector3 finalTarget = new Vector3();

            if (hero.RewardResources[0].activeSelf)
            {
                SymbolMovementToTarget(0, hero, finalTarget, lerpedScreenPos, progress);
            }

            if (hero.RewardResources[1].activeSelf)
            {
                SymbolMovementToTarget(1, hero, finalTarget, lerpedScreenPos, progress);
            }

            if (hero.RewardResources[2].activeSelf)
            {
                SymbolMovementToTarget(2, hero, finalTarget, lerpedScreenPos, progress);
            }

            yield return null;
        }
    }

    private void SymbolMovementToTarget(int index, Hero hero, Vector3 finalTarget, Vector3 lerpedScreenPos, float progress)
    {
        hero.RewardResources[index].transform.localScale = new Vector3(1.5f, 1.5f);

        finalTarget = rewardTargets[index].transform.position;

        hero.RewardResources[index].transform.position = Vector3.Lerp(lerpedScreenPos, finalTarget, progress);
    }

    private IEnumerator GiveSymbolReward(Hero hero, Vector3 screenPosition, float maxTimer = 0.7f, float duration = 0f)
    {
        while (duration < maxTimer)
        {
            duration += Time.deltaTime;

            float progress = duration / maxTimer;

            if (hero.RewardResources[0].activeSelf)
            {
                PlaceResourceAtPosition(0, hero, progress);
            }

            if (hero.RewardResources[1].activeSelf)
            {
                PlaceResourceAtPosition(1, hero, progress);
            }

            if (hero.RewardResources[2].activeSelf)
            {
                PlaceResourceAtPosition(2, hero, progress);
            }

            yield return null;
        }
    }

    private void PlaceResourceAtPosition(int index, Hero hero, float progress)
    {
        FillImage(index, progress);

        ChangeSymbolObjectScale(hero, index, progress);
    }

    private void FillImage(int index, float progress)
    {
        Image rewardImage = fillingImage[index];

        rewardImage.fillAmount = Mathf.Lerp(rewardImage.fillAmount, 1, progress);

        Color originalColor = new Color(rewardImage.color.r, rewardImage.color.g, rewardImage.color.b, rewardImage.color.a);

        rewardImage.color = originalColor;

        originalColor.a = 1f;

        Color newColor = new Color(rewardImage.color.r, rewardImage.color.g, rewardImage.color.b, 255);

        Color finalColor = Color.Lerp(originalColor, newColor, progress);

        rewardImage.color = finalColor;
    }

    private void ChangeSymbolObjectScale(Hero hero, int index, float progress)
    {
        GameObject rewardObject = hero.RewardResources[index];

        Vector3 originalScale = rewardObject.transform.localScale;

        rewardObject.transform.localScale = originalScale;

        Vector3 newScale = new Vector3(0f, 0f, originalScale.z);

        Vector3 lerpedScale = Vector3.Lerp(originalScale, newScale, progress);

        rewardObject.transform.localScale = lerpedScale;
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
