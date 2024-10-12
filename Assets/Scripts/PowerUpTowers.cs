using SpaceShooter;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PowerUpTowers : MonoBehaviour
{
    [SerializeField] private float radius;
     public float Radius => radius;
    [SerializeField] private float addDamage;
    public float AddDamage => addDamage;




#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.green;
        Handles.DrawWireDisc(transform.position, Vector3.forward, radius);
    }
#endif
}
