using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;
using System;
using UnityEngine.Events;
public class TDPatrolController : AIController
{
    private Path m_Path;
    private int pathIndex = 0;
    [SerializeField] private UnityEvent OnEndPath; 

    public void SetPath(Path path)
    {
        m_Path = path;
        SetPatrolBehaviour(path[pathIndex]);
    }

    protected override void GetNewPoint()
    {
        pathIndex++;
        if (m_Path.Length > pathIndex)
        {
            SetPatrolBehaviour (m_Path[pathIndex]);
        }
        else
        {

            OnEndPath.Invoke();


            Destroy(gameObject);
        }
    }
}
