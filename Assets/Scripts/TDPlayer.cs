using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;
using System;
public class TDPlayer : Player
{
    [SerializeField] private UpgradeAsset healthUpgrade;
    private new void Awake()
    {
        base.Awake();
        int level = Upgrades.GetUpgradeLevel(healthUpgrade);
        TakeDamage(-level * 5);
    }
    public static new TDPlayer Instance {get{ return (Player.Instance as TDPlayer);} }
    public static void GoldUpdateSubscribe(Action<int> act)
    {
        OnGoldUpdate += act;
        //if(Instance != null)
            OnGoldUpdate(Instance.m_Gold);
    }

    public static void LifeUpdateSubscribe(Action<int> act)
    {
        OnLifeUpdate += act;
        OnLifeUpdate(Instance.NumLives);
    }

    private static event Action<int> OnGoldUpdate;
    public static event Action<int> OnLifeUpdate;


    [SerializeField] private int m_Gold = 0;
    public int Gold => m_Gold;
    public void ChangeGold(int change)
    {
        m_Gold += change;
        OnGoldUpdate(m_Gold);

    }



    public void ReduceLife(int change)
    {
        TakeDamage(change);
        OnLifeUpdate(NumLives);
    }

    [SerializeField] private Tower m_TowerPrefab;

    public void TryBuild(TowerAsset m_TowerAsset, Transform m_BuildSite)
    {
        if(m_TowerAsset.goldCost <= m_Gold)
            ChangeGold(-m_TowerAsset.goldCost);
        Tower tower = Instantiate(m_TowerPrefab, m_BuildSite.position, Quaternion.identity);
        tower.GetComponentInChildren<SpriteRenderer>().sprite = m_TowerAsset.sprite;
        tower.GetComponent<Tower>().Radius = m_TowerAsset.radius;
        tower.Gold = m_TowerAsset.goldCost; 
        Destroy(m_BuildSite.gameObject);
        AssignTurret(tower, m_TowerAsset.turretAsset);
    }



    private void AssignTurret(Tower tower, TurretProperties turret)
    {
        Turret t = tower.GetComponentInChildren<Turret>();
        t.AssignLoadout(turret);
    }
}
