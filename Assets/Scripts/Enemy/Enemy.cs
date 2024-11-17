using SpaceShooter;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(TDPatrolController))]
public class Enemy : MonoBehaviour
{
    public enum ArmorType
    {
        Base = 0,
        Magi = 1
    }

    private static Func<int, TDProjectile.DamageType, int, int>[] ArmorDamageFunctions =
    {
        (int power, TDProjectile.DamageType type, int armor) =>
        {//Base armor
            switch(type)
            {
                case TDProjectile.DamageType.Mage: return power;
                default: return Mathf.Max(1, power - armor);
            }
        },

        (int power, TDProjectile.DamageType type, int armor) =>
        {//magic armor
            if( TDProjectile.DamageType.Base == type ) 
                armor /= 2;
            
            return Mathf.Max(1, power - armor);
        }
    };
        


    [SerializeField] private int m_Damage = 1;
    [SerializeField] private int m_Gold = 1;
    [SerializeField] private int m_Armor = 1;
    [SerializeField] private ArmorType armorType;
    private Destructible destructible;


    private void Awake()
    {
        destructible = GetComponent<Destructible>();
    }

    public int addGold = 0;
    public event Action OnEnd;

    private void OnDestroy()
    {
        Sound.EnemyDie.Play();
        OnEnd?.Invoke();
    }


    public void Use(EnemyAsset asset)
    {
        SpriteRenderer sr = transform.Find("View").GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = asset.color;
            sr.transform.localScale = new Vector3(asset.spriteScale.x, asset.spriteScale.y, 1);

            sr.GetComponent<Animator>().runtimeAnimatorController = asset.animations;


            GetComponent<SpaceShip>().Use(asset);
            m_Damage = asset.damage;
            m_Gold = asset.gold;
            m_Armor = asset.armor;
            armorType = asset.armorType;
        }
        CircleCollider2D collider = transform.Find("Collider").GetComponent<CircleCollider2D>();
        if (collider != null)
        {
            collider.radius = asset.radius;
        }


    }

    public void DamagePlayer()
    {
       TDPlayer.Instance.ReduceLife(m_Damage);   
    }
    public void GivePlayerGold()
    {
        TDPlayer.Instance.ChangeGold(m_Gold + addGold);
    }


    public void TakeDamage(int damage, TDProjectile.DamageType damageType)
    {
        destructible.ApplyDamage(ArmorDamageFunctions[(int)armorType](damage, damageType, m_Armor));
    }
}
