using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField]
    private ZombieHealth zombiePrefab;
    [SerializeField]
    private ZombieData[] zombieDatas;

    [SerializeField]
    private Transform[] spawnPoints;

    private List<ZombieHealth> waveZombies = new List<ZombieHealth>();

    private void Update()
    {
        UIManager.Instance.SetEnemyWaveText(GameManager.Instance.Wave, waveZombies.Count);

        // 한 웨이브 끝남
        if(waveZombies.Count <= 0)
        {
            GameManager.Instance.ClearWave();
            ZombieSpawnLogic();
        }
    }

    private void ZombieSpawnLogic()
    {
        int zombieSpawnCount = GameManager.Instance.Wave * 2;

        for (int i = 0; i < zombieSpawnCount; i++)
        {
            SpawnZombie();
        }
    }

    private void SpawnZombie()
    {
        int randPoint = UnityEngine.Random.Range(0, spawnPoints.Length);
        ZombieHealth spawn = Instantiate(zombiePrefab, spawnPoints[randPoint].position, spawnPoints[randPoint].rotation);

        spawn.OnDeath += () =>
        {
            waveZombies.Remove(spawn);
            GameManager.Instance.SetScore(GameManager.Instance.Score + 1);
        };


        waveZombies.Add(spawn);
    }
}
