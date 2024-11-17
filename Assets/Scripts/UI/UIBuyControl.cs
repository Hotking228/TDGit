using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIBuyControl : MonoBehaviour
{
    [SerializeField] private UITowerBuyControl m_TowerBuyPrefab;
    
    private List<UITowerBuyControl> m_ActiveControl;
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

    private void MoveToBuildSite(BuildSite buildSite)
    {
        if (gameObject == null) return;
        if (buildSite != null)
        {
            
            Vector2 position = Camera.main.WorldToScreenPoint(buildSite.transform.root.position);

                rectTransform.anchoredPosition = position;
                gameObject.SetActive(true);
            m_ActiveControl = new List<UITowerBuyControl>();
            for (int i = 0; i < buildSite.BuildableTowers.Length; i++)
            {
                
                if (buildSite.BuildableTowers[i].IsAvailable())
                {
                    var newControl = Instantiate(m_TowerBuyPrefab, transform);
                    m_ActiveControl.Add(newControl);
                    newControl.SetTowerAsset(buildSite.BuildableTowers[i]);
                }
                
            }
            var angle = 360 / m_ActiveControl.Count;
            for (int i = 0; i < m_ActiveControl.Count; i++)
            {
                var offset = Quaternion.AngleAxis(angle * i, Vector3.forward) * Vector3.up * 80; 
                m_ActiveControl[i].transform.position += offset;
            }

        }
        else
        {
            if(m_ActiveControl!= null)
            foreach (var control in m_ActiveControl) Destroy(control?.gameObject);
            gameObject.SetActive(false);
        }
        var tbcs = GetComponentsInChildren<UITowerBuyControl>();
        foreach (var tbc in tbcs)
        {
            tbc.SetterBuildSite = buildSite?.transform?.root;
        }
    }
    private void OnDestroy()
    {
        BuildSite.OnClickEvent -= MoveToBuildSite;
    }
}
