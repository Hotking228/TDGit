using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Скрипт прожектайла. Кидается на топ префаба прожектайла.
    /// </summary>
    public class Projectile : Entity
    {
        public enum FollowEnemyMode
        {
            None,
            follow
        }

        public enum DamageMode
        {
            Area,
            Single
        }
        [SerializeField] private DamageMode damageMode = DamageMode.Single;
        [SerializeField] private FollowEnemyMode followMode = FollowEnemyMode.None;
        [SerializeField] private string m_AnimTransition = "collide";
        [SerializeField] private Animator m_Animator;

        private Transform target;
        public Transform Target { set { target = value; } }
        /// <summary>
        /// Линейная скорость полета снаряда.
        /// </summary>
        [SerializeField] private float m_Velocity;

        /// <summary>
        /// Время жизни снаряда.
        /// </summary>
        [SerializeField] private float m_Lifetime;

        /// <summary>
        /// Повреждения наносимые снарядом.
        /// </summary>
        [SerializeField] private int m_Damage;
        [SerializeField] private float addDamage = 0;

        /// <summary>
        /// Эффект попадания от что то твердое. 
        /// </summary>
        [SerializeField] private ImpactEffect m_ImpactEffectPrefab;

        private float m_Timer;
        private void Start()
        {
            PowerUpTowers[] powerups = FindObjectsByType<PowerUpTowers>(FindObjectsSortMode.None);
            for (int i = 0; i < powerups.Length; i++)
            {
                if(Vector2.Distance(transform.position, powerups[i].transform.position) <= powerups[i].Radius)
                addDamage += (int)(powerups[i].AddDamage * m_Damage);
            }
        }
        private void Update()
        {
            float stepLength = Time.deltaTime * m_Velocity;
            Vector2 step = transform.up * stepLength;
            RaycastHit2D hit = new RaycastHit2D();
            RaycastHit2D[] hits = new RaycastHit2D[0];
            if(damageMode == DamageMode.Single)
                hit = Physics2D.Raycast(transform.position, transform.up, stepLength);
            if (damageMode == DamageMode.Area)
                hits = Physics2D.CircleCastAll(transform.position, stepLength*2f, transform.up);
            // не забыть выключить в свойствах проекта, вкладка Physics2D иначе не заработает
            // disable queries hit triggers
            // disable queries start in collider
            if (hit || hits.Length != 0)
            {
                if(damageMode == DamageMode.Single)
                    OnHit(hit);
                if(damageMode == DamageMode.Area)
                {
                    foreach(var h in hits)
                    {
                        OnHit(h);
                    }
                }
                    

                OnProjectileLifeEnd(hit.collider, hit.point);
            }

            m_Timer += Time.deltaTime;

            if(m_Timer > m_Lifetime)
                Destroy(gameObject);
                transform.position += new Vector3(step.x, step.y, 0);
            if (followMode == FollowEnemyMode.follow && target != null)
                transform.up = (target.position - transform.position).normalized;
        }

        private void OnHit(RaycastHit2D hit)
        {
            var destructible = hit.collider.transform.root.GetComponent<Destructible>();

            if (destructible != null && destructible != m_Parent)
            {
                if (m_Animator != null)
                    m_Animator.SetBool(m_AnimTransition, true);
                destructible.ApplyDamage(m_Damage + (int)addDamage);

                // #Score
                // добавляем очки за уничтожение
                if (Player.Instance != null && destructible.HitPoints < 0)
                {
                    // проверяем что прожектайл принадлежит кораблю игрока. 
                    // здесь есть нюанс - если мы выстрелим прожектайл и после умрем
                    // то новый корабль игрока будет другим, в случае если прожектайл запущенный из предыдущего шипа
                    // добьет то очков не дадут. Можно отправить пофиксить на ДЗ. (например тупо воткнув флаг что прожектайл игрока)
                    if (m_Parent == Player.Instance.ActiveShip)
                    {
                        Player.Instance.AddScore(destructible.ScoreValue);
                    }
                }
            }

            
        }

        private void OnProjectileLifeEnd(Collider2D collider, Vector2 pos)
        {
            if(m_ImpactEffectPrefab != null)
            {
                var impact = Instantiate(m_ImpactEffectPrefab.gameObject);
                impact.transform.position = pos;
            }

            Destroy(gameObject);
        }


        private Destructible m_Parent;

        public void SetParentShooter(Destructible parent)
        {
            m_Parent = parent;
        }
    }
}

