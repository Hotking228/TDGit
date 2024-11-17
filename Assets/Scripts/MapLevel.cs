using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;
using TMPro;
using UnityEngine.UI;
using System;

public class MapLevel : MonoBehaviour
{
    [SerializeField]private Episode episode;
    public Episode Ep => episode;
    public bool IsComplete { get { return gameObject.activeSelf &&  resultPanel.gameObject.activeSelf; } }


    [SerializeField] private RectTransform resultPanel;
    [SerializeField] private Image[] resultImages;
    public void LoadLevel()
    {
        Debug.Log("Click");
        if (episode != null)
        LevelSequenceController.Instance.StartEpisode(episode);
    }


    public void Initialise()
    {
        var score = MapCompletion.Instance.GetEpisodeScore(episode);
        resultPanel.gameObject.SetActive(score > 0);
        for (int i = 0; i < score; i++)
        {
            resultImages[i].color = Color.white;
        }
    }
}
