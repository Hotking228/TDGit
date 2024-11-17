using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITowerBuyControl : MonoBehaviour
{
    public void SetTowerAsset(TowerAsset asset)
    {
        m_TowerAsset = asset;
    }


    [SerializeField] private TextMeshProUGUI m_Text;
    [SerializeField] private Button m_Button;
    [SerializeField] private Transform m_BuildSite;

    public Transform SetterBuildSite { set { m_BuildSite = value; } }

    [SerializeField] private TowerAsset m_TowerAsset;

    private void Awake()
    {
        
        
    }
    private void Start()
    {
        TDPlayer.GoldUpdateSubscribe(GoldStatusCheck);
        m_Text.text = m_TowerAsset.goldCost.ToString();
        m_Button.GetComponent<Image>().sprite = m_TowerAsset.GUISprite;
    }
    private void GoldStatusCheck(int gold)
    {
        if (gold >= m_TowerAsset.goldCost != m_Button.interactable)
        {
            if (m_Button == null) return;
            m_Button.interactable = !m_Button.interactable;
            m_Text.color = m_Button.interactable ? Color.white : Color.red;
        }
    }

    public void Buy()
    {
        Sound.Build.Play();
        TDPlayer.Instance.TryBuild(m_TowerAsset, m_BuildSite);
        BuildSite.HideControls();
    }

}
