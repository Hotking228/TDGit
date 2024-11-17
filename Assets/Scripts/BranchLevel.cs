using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[RequireComponent(typeof(MapLevel))]
public class BranchLevel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointText;
    [SerializeField] private MapLevel rootLevel;
    [SerializeField] private int needPoints = 3;


    internal void TryActivate()
    {
        gameObject.SetActive(rootLevel.IsComplete);

        if (needPoints > MapCompletion.Instance.TotalScore)
        {
            pointText.text = needPoints.ToString();
            
        }
        else
        {
            pointText.transform.parent.gameObject.SetActive(false);
            GetComponent<MapLevel>().Initialise();
        }
    }
}
