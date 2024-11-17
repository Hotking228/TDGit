using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sound
{
    BGM,
    Arrow,
    ArrowHit,
    Build,
    EnemyDie
}

public static class SoundExtensions
{
    public static void Play(this Sound sound)
    {
        SoundPlayer.Instance.Play(sound);
    }
}