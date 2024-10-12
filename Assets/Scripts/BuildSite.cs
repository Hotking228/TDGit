using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildSite : MonoBehaviour, IPointerDownHandler
{
    public static event Action<Transform> OnClickEvent;


    public virtual void OnPointerDown(PointerEventData eventData)
    {
        OnClickEvent(transform.root);
    }

    public static void HideControls()
    {
        OnClickEvent(null);
    }
}
