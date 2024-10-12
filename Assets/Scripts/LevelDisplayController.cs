using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDisplayController : MonoBehaviour
{
    [SerializeField] private MapLevel[] levels;

    // Start is called before the first frame update
    void Start()
    {
        int drawLevel = 0;

        while (drawLevel < levels.Length && MapCompletion.Instance.TryIndex(drawLevel, out var episode, out var score))
        {
            levels[drawLevel++].SetLevelData(episode, score);
            if(score == 0)
                break; 
        }

        for (int i = drawLevel; i < levels.Length; i++)
        {
           
            levels[i].gameObject.SetActive(false);
        }
    }
}