using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickProtection : MonoSingleton<ClickProtection>, IPointerClickHandler
{
    private Image blocker;

    private void Start()
    {
        blocker = GetComponent<Image>();
    }
    private Action<Vector2> OnClickAction;
    public void Activate(Action<Vector2> mouseAction)
    {
        blocker.enabled = true;
        OnClickAction = mouseAction;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        blocker.enabled = false;
        OnClickAction(eventData.pressPosition);
        OnClickAction = null;
    }
}
