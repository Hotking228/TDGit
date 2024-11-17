using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class LevelDisplayController : MonoBehaviour
{
    [SerializeField] private MapLevel[] levels;
    [SerializeField] private BranchLevel[] branchLevels;
    // Start is called before the first frame update
    void Start()
    {
        int drawLevel = 0;

        while (drawLevel < levels.Length )
        {
            if (drawLevel != 0 && MapCompletion.Instance.GetEpisodeScore(levels[drawLevel - 1].Ep) == 0)
                break;
            levels[drawLevel++].Initialise();
            
        }

        for (int i = drawLevel; i < levels.Length; i++)
        {
           
            levels[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < branchLevels.Length; i++)
        {

            branchLevels[i].TryActivate();
        }



    }
}
