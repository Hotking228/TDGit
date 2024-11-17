using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    public static event Action<Enemy> OnEnemySpawn;
    [SerializeField] private Path[] paths;
    [SerializeField] private EnemyWave currentWave;
    [SerializeField] private Enemy m_EnemyPrefab;
    [SerializeField] private UpgradeAsset upgradeAsset; 
    private int activeEnemyCount = 0;

    public event Action OnAllWavesDead;

    private void RecordEnemyDead()
    {
        if (--activeEnemyCount == 0)
        {

                ForceNextWave();

        }
    }




    private void Start()
    {
       

        currentWave.Prepare(SpawnEnemies);
    }

    private void SpawnEnemies()
    {
        foreach((EnemyAsset asset, int count, int pathIndex) in currentWave.EnumerateSquads())
        {
            if (pathIndex < paths.Length)
            {
                for (int i = 0; i < count; i++)
                {
                    var e = Instantiate(m_EnemyPrefab, paths[pathIndex].startArea.RandomInsideZone, Quaternion.identity);
                    e.Use(asset);
                    e.GetComponent<TDPatrolController>().SetPath(paths[pathIndex]);
                    e.addGold = Upgrades.GetUpgradeLevel(upgradeAsset);
                    activeEnemyCount++;
                    e.OnEnd += RecordEnemyDead;
                    OnEnemySpawn?.Invoke(e);
                }
            }
        }



        currentWave = currentWave.PrepareNext(SpawnEnemies);

    }

    public void ForceNextWave()
    {
        if (currentWave)
        {
            TDPlayer.Instance.ChangeGold((int)currentWave.GetRemainingTime());
            SpawnEnemies();
        }
        else 
        {
            if (activeEnemyCount == 0)
                OnAllWavesDead?.Invoke();
        }
    }
}
