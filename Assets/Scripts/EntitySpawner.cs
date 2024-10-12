using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;


public class EntitySpawner : Spawner
{
    [SerializeField] private GameObject[] m_EntityPrefabs;
    protected override GameObject GenerateSpawnEntity()
    {
        return Instantiate( m_EntityPrefabs[Random.Range(0, m_EntityPrefabs.Length)]);
    }
}
