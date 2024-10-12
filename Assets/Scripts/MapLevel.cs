using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;
using TMPro;

public class MapLevel : MonoBehaviour
{
    private Episode episode;
    [SerializeField] private TextMeshProUGUI text;

    public void LoadLevel()
    {
        if (episode != null)
        LevelSequenceController.Instance.StartEpisode(episode);
    }
    public void SetLevelData(Episode episode, int score)
    {
        this.episode = episode;
        text.text = $"{score}/3";
    }
}
