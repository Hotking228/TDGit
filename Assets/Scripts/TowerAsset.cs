using SpaceShooter;
using UnityEngine;
[CreateAssetMenu]

public class TowerAsset : ScriptableObject
{
    public int goldCost = 15;
    public Sprite GUISprite;
    public Sprite sprite;
    public float radius = 2.5f;
    public TurretProperties turretAsset;
    [SerializeField]private UpgradeAsset requiredUpgrade;
    [SerializeField] private int requiredUpgradeLevel;

    public bool IsAvailable()
    {
        if (requiredUpgrade)
            return requiredUpgradeLevel <= Upgrades.GetUpgradeLevel(requiredUpgrade);
        return true;
    }


    public TowerAsset[] m_UpgradesTo;
}
