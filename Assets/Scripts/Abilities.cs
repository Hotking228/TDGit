using SpaceShooter;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Abilities : MonoSingleton<Abilities>
{

    [SerializeField] private UpgradeAsset slowTimeAsset;
    [SerializeField] private UpgradeAsset fireAsset;
    [SerializeField] private Button timeButton;
    [SerializeField] private Button fireButton;

    [Serializable]
    public class FireAbility 
    {
        
        [SerializeField] private float cost = 5;
        public float Cost => cost;
        [SerializeField] private int damageBase;
        [SerializeField] private Color targetingColor; 
        public void Use()
        {
            MyBag.Instance.ChangeMana(cost);
            ClickProtection.Instance.Activate((Vector2 v) => 
            { 
                Vector2 position = Camera.main.ScreenToWorldPoint(v);
                foreach( var collider in Physics2D.OverlapCircleAll(position, 5))
                {
                    if(collider.transform.root.TryGetComponent<Enemy>(out var enemy))
                    {
                        enemy.TakeDamage(damageBase, TDProjectile.DamageType.Mage);
                    }
                }

            });
        }

        public void SetDamage(int damage)
        {
            damageBase = damage * damageBase;
        }
    }

    

    [Serializable]
    public class TimeAbility 
    {
        [SerializeField] private int cost = 10;
        public float Cost => cost;
        [SerializeField] private float coolDown = 15;
        [SerializeField] private float durationBase;
        
        public void Use()
        {
            MyBag.Instance.ChangeMana(cost);
            void Slow(Enemy e)
            {
                
                e.GetComponent<SpaceShip>().HalfMaxLinearVelocity();
            }

            IEnumerator Restore()
            {
                yield return new WaitForSeconds(durationBase);

                foreach (var ship in FindObjectsByType<SpaceShip>(FindObjectsSortMode.None))
                    ship.RestoreMaxLinearVelocity();
                EnemyWaveManager.OnEnemySpawn -= Slow;

            }


            foreach (var ship in FindObjectsByType<SpaceShip>(FindObjectsSortMode.None))
                ship.HalfMaxLinearVelocity();

            EnemyWaveManager.OnEnemySpawn +=  Slow; 
            Instance.StartCoroutine(Restore());

            IEnumerator TimeAbilityButton()
            {
                Instance.timeButton.interactable = false;
                yield return new WaitForSeconds(coolDown);
                Instance.timeButton.interactable = true;
            }

            Instance.StartCoroutine(TimeAbilityButton());
        }
        public void SetDuration(int duration)
        {
            durationBase = duration * durationBase;
        }
    }

    [SerializeField] private FireAbility fireAbility;
    [SerializeField] private TimeAbility timeAbility;
    [SerializeField] private Image targetingCircle;


    public void UseFireAbility()
    {
        fireAbility.Use();
    }

    public void UseTimeAbility()
    {
        timeAbility.Use();
    }
    private int fireDamage;
    private int slowDuration;

    private void Start()
    {
        fireButton.interactable = (fireDamage = Upgrades.GetUpgradeLevel(fireAsset)) != 0;
        timeButton.interactable = (slowDuration = Upgrades.GetUpgradeLevel(slowTimeAsset)) != 0;
        fireAbility.SetDamage(fireDamage);
        timeAbility.SetDuration(slowDuration);


    }
    private void Update()
    {

        fireButton.interactable = MyBag.Instance.CurrentMana >= fireAbility.Cost;
        timeButton.interactable = MyBag.Instance.CurrentMana >= timeAbility.Cost;

    }


}
