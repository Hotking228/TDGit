using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildSite : MonoBehaviour, IPointerDownHandler
{
    public static event Action<BuildSite> OnClickEvent;
    [SerializeField] private TowerAsset[] m_BuildableTowers; 
    public TowerAsset[] BuildableTowers { get { return m_BuildableTowers; } }

    


    public virtual void OnPointerDown(PointerEventData eventData)
    {
        OnClickEvent(this);
    }

    public static void HideControls()
    {
        OnClickEvent(null);
    }
}
