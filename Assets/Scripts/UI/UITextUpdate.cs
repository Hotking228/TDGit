using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITextUpdate : MonoBehaviour
{
    public enum UpdateSource
    {
        Gold,
        Life
    }

    public UpdateSource source;

    private TextMeshProUGUI m_Text;
    private void Awake()
    {
        m_Text = GetComponent<TextMeshProUGUI>();

        
    }
    private void Start()
    {
        if (source == UpdateSource.Gold)
            TDPlayer.GoldUpdateSubscribe(UpdateText);
        if (source == UpdateSource.Life)
            TDPlayer.LifeUpdateSubscribe(UpdateText);
    }
    private void UpdateText(int value)
    {
        m_Text.text = value.ToString();
    }
}
