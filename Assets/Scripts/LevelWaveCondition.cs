using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;

public class LevelWaveCondition : MonoBehaviour, ILevelCondition
{
    private bool isCompleted;
    public bool IsCompleted { get { return isCompleted; } }
    private void Start()
    {
        FindAnyObjectByType<EnemyWaveManager>().OnAllWavesDead += () => { isCompleted = true; };
    }
}
