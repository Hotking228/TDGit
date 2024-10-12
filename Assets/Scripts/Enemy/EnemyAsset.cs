using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public sealed class EnemyAsset : ScriptableObject
{
    [Header("View")]
    public Color color = Color.white;
    public Vector2 spriteScale = new Vector2(3, 3);
    public RuntimeAnimatorController animations;

    [Header("Params")]
    public float moveSpeed = 1;
    public int hp = 1;
    public int score = 1;
    public float radius;
    public int damage = 1;
    public int gold = 1;
}
