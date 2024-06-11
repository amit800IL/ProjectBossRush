using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource playerAttack;
    [SerializeField] private AudioSource playerDefend;
    [SerializeField] private AudioSource bossAttack;

    private void Start()
    {
        Hero.OnHeroAttack += ActivatePlayerAttackSound;
        Hero.OnHeroDefend += ActivatePlayerDefendSound;
    }

    private void OnDisable()
    {
        Hero.OnHeroAttack -= ActivatePlayerAttackSound;
        Hero.OnHeroDefend -= ActivatePlayerDefendSound;
    }

    public void ActivatePlayerAttackSound(Hero hero)
    {
        Debug.Log("Player attack sound played");

        if (hero != null)
            playerAttack.Play();
    }

    public void ActivatePlayerDefendSound(Hero hero)
    {
        Debug.Log("Player defend sound played");

        if (hero != null)
            playerDefend.Play();
    }

    public void ActivateBossAttackSound()
    {
        Debug.Log("boss attack sound played");

        bossAttack.Play();
    }
}
