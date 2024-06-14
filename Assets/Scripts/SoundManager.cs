using System;
using System.Collections;
using System.Collections.Generic;
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

    public void ActivatePlayerAttackSound()
    {
        Debug.Log("Player attack sound played");

        playerAttack.Play();
    }

    public void ActivatePlayerDefendSound()
    {
        Debug.Log("Player defend sound played");

        playerDefend.Play();
    }

    public void ActivateBossAttackSound()
    {
        Debug.Log("boss attack sound played");

        bossAttack.Play();
    }
}
