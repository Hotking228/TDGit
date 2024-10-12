using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;

public class TDLevelController : LevelController
{
    public int levelScore = 1;
    private new void Start()
    {
        base.Start();
        TDPlayer.Instance.OnPlayerDead += () => { StopLevelActivity(); LevelResultController.Instance.Show(false); };
        m_EventLevelCompleted.AddListener(()=>
        {
            StopLevelActivity();
            MapCompletion.SaveEpisodeResult(levelScore);
        }
        );
    }


    private void StopLevelActivity()
    {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        foreach(var enemy in enemies)
        {
            enemy.GetComponent<SpaceShip>().enabled = false;
            enemy.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }

        DisavleAll<Spawner>();
        DisavleAll<Tower>();
        DisavleAll<Projectile>();
        
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
