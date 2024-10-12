using SpaceShooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(TDPatrolController))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int m_Damage = 1;
    [SerializeField] private int m_Gold = 1;
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
        TDPlayer.Instance.ChangeGold(m_Gold);
    }
}
