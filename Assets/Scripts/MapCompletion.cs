using SpaceShooter;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCompletion : MonoSingleton<MapCompletion>
{
    public const string fileName = "completion.dat";



    [Serializable]
    private class EpisodeScore
    {
        public Episode episode;
        public int score;
    }
    public static void SaveEpisodeResult(int levelScore)
    {
        if(Instance)
            Instance.SaveResult(LevelSequenceController.Instance.CurrentEpisode, levelScore);
    }

    [SerializeField] private EpisodeScore[] completionData;
    [SerializeField] private int totalScore;
    public int TotalScore { get { return totalScore; } }
    private new void Awake()
    {
        base.Awake();
        Saver<EpisodeScore[]>.TryLoad(fileName, ref completionData);
        foreach (var episodeScore in completionData)
        {
            totalScore += episodeScore.score;
        }
    }
    private void SaveResult(Episode currentEpisode, int levelScore)
    {
        foreach (var item in completionData)
        {
            if (item.episode == currentEpisode)
            {
                if (levelScore > item.score)
                {
                    totalScore += levelScore - item.score;
                    item.score = levelScore;
                    Saver<EpisodeScore[]>.Save(fileName, completionData);
                }

            }
        }
    }

    public int GetEpisodeScore(Episode episode)
    {
        foreach (var data in completionData)
        {
            if (data.episode == episode)
            {
                return data.score;
            }
        }
        return 0;
    }
}
