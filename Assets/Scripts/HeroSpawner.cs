using System.Collections.Generic;
using UnityEngine;

public class HeroSpawner : MonoBehaviour
{
    [SerializeField] List<HeroToSpawn> heroToSpawns = new List<HeroToSpawn>();

    private void Start()
    {
        foreach (HeroToSpawn hero in heroToSpawns)
        {
            Instantiate(hero.HeroToSpawnObject, hero.SpawnPosition, hero.HeroToSpawnObject.transform.rotation);
        }
    }
}

[System.Serializable]
public class HeroToSpawn
{
    [SerializeField] private GameObject heroToSpawnObject;
    [SerializeField] private Vector2 spawnPosition;
    public GameObject HeroToSpawnObject { get => heroToSpawnObject; private set => heroToSpawnObject = value; }
    public Vector2 SpawnPosition { get => spawnPosition; private set => spawnPosition = value; }
}
