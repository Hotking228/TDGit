using SpaceShooter;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] private AIPointPatrol[] points;

    public int Length { get =>  points.Length; }
    public AIPointPatrol this[int i] { get => points[i]; }


#if UNITY_EDITOR
    private static readonly Color GizmoColor = new Color(0, 1, 0, 0.3f);

    private void OnDrawGizmosSelected()
    {

        foreach (var point in points)
        {

            Handles.color = GizmoColor;
            Handles.DrawSolidDisc(point.transform.position, transform.forward, 0.3f);
        }
    }
#endif
}
