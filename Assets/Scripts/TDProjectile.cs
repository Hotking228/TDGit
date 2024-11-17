using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;
public class TDProjectile : Projectile
{
    public enum DamageType
    {
        Base,
        Mage
    }
    public DamageType damageType;
    protected override void OnHit(RaycastHit2D hit)
    {

            var enemy = hit.collider.transform.root.GetComponent<Enemy>();

            if (enemy != null && enemy != m_Parent)
            {
                enemy.TakeDamage(m_Damage + (int)addDamage + addDamageUpgrade, damageType);


            }
    }
}
