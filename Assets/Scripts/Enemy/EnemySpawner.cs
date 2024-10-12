using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;



public class EnemySpawner : Spawner
{
    [SerializeField] private Enemy m_EnemyPrefab;

    [SerializeField] private Path path;

    [SerializeField] private EnemyAsset[] enemySettings;
    protected override GameObject GenerateSpawnEntity()
    {
        var e = Instantiate(m_EnemyPrefab);
        e.Use(enemySettings[Random.Range(0, enemySettings.Length)]);
        e.GetComponent<TDPatrolController>().SetPath(path);
        return e.gameObject;
    }
}
