using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UpgradeAsset : ScriptableObject
{
    public string nameUpgrade;
    public Sprite sprite;

    public int[] costByLevel = { 3 };
}
