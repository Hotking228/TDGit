using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIBuyControl : MonoBehaviour
{ 

    private RectTransform rectTransform;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
            BuildSite.OnClickEvent += MoveToBuildSite;
        gameObject.SetActive(false);
    }
    private void Start()
    {
        
    }

    private void MoveToBuildSite(Transform buildSite)
    {
        if (gameObject == null) return;
        if (buildSite != null)
        {
            
            Vector2 position = Camera.main.WorldToScreenPoint(buildSite.position);

                rectTransform.anchoredPosition = position;
                gameObject.SetActive(true);

        }
        else
        {
            gameObject.SetActive(false);
        }
        var tbcs = GetComponentsInChildren<UITowerBuyControl>();
        foreach (var tbc in tbcs)
        {
            tbc.SetterBuildSite = buildSite;
        }
    }
    private void OnDestroy()
    {
        BuildSite.OnClickEvent -= MoveToBuildSite;
    }
}
