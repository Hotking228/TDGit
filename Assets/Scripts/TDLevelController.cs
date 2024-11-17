using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;

public class TDLevelController : LevelController
{
    private int levelScore = 3;
    private new void Start()
    {
        base.Start();
        TDPlayer.Instance.OnPlayerDead += () => { StopLevelActivity(); LevelResultController.Instance.Show(false); };
        m_ReferenceTime += Time.time;
        m_EventLevelCompleted.AddListener(()=>
        {
            StopLevelActivity();
            if (m_ReferenceTime <= Time.time)
                levelScore -= 1;
 

            MapCompletion.SaveEpisodeResult(levelScore);
        }
        );

        void LifeScoreChange(int _)
        {
            if(_ >= 0) return;
            levelScore -= 1;
            TDPlayer.OnLifeUpdate -= LifeScoreChange;
        }
        TDPlayer.OnLifeUpdate += LifeScoreChange;
    }


    private void StopLevelActivity()
    {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        foreach(var enemy in enemies)
        {
            enemy.GetComponent<SpaceShip>().enabled = false;
            enemy.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }

        DisavleAll<EnemyWave>();
        DisavleAll<Tower>();
        DisavleAll<Projectile>();
        DisavleAll<UINextWave>();
        
    }

    private void DisavleAll<T>() where T : MonoBehaviour
    {
        T[] objects = FindObjectsByType<T>(FindObjectsSortMode.None);
        foreach (var obj in objects)
        {
            obj.enabled = false;
        }
    }

}
