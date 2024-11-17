using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Sounds : ScriptableObject
{
    public AudioClip[] m_Sounds;

    public AudioClip this[Sound s] => m_Sounds[(int)s];
}