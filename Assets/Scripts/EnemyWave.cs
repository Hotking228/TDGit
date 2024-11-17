using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{

    public static event Action<float> OnWavePrepare;
    [Serializable]
    private class Squad
    {
        public EnemyAsset asset;
        public int count;
    }
    [Serializable]
    private class PathGroup
    {
        public Squad[] squads;
    }

    [SerializeField] private PathGroup[] groups;    

    [SerializeField] private float prepareTime = 10;
    private void Awake()
    {
        enabled = false;
    }

    private event Action OnWaveReady;


    public float GetRemainingTime()
    {
        return prepareTime - Time.time ;
    }



    public void Prepare(Action spawnEnemies)
    {
        OnWavePrepare?.Invoke(prepareTime);
        prepareTime += Time.time;
        enabled = true;
        OnWaveReady += spawnEnemies;
    }
    private void Update()
    {
        if (Time.time >= prepareTime)
        {
            enabled = false;
            OnWaveReady?.Invoke();
        }
    }

    public IEnumerable<(EnemyAsset asset, int count, int pathIndex)> EnumerateSquads()
    {
        for (int i = 0; i < groups.Length; i++)
        {
            foreach (var squad in groups[i].squads)
            {
                yield return (squad.asset, squad.count, i);
            }
        }
        
    }

    [SerializeField] private EnemyWave next;
    public EnemyWave PrepareNext(Action spawnEnemies)
    {
        OnWaveReady -= spawnEnemies;
        next?.Prepare(spawnEnemies);
        return next;
    }
}
