using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyUpgrade : MonoBehaviour
{
    [SerializeField] private UpgradeAsset asset;
    [SerializeField] private Image upgradeIcon;
    [SerializeField] private TextMeshProUGUI level, cost;
    [SerializeField] private Button buyButton;
    private int costNumber = 0;
    private void Start()
    {
        upgradeIcon.sprite = asset.sprite;
    }
    public void Initialize( int level = 1)
    {
        upgradeIcon.sprite = asset.sprite;
        var savedLevel = Upgrades.GetUpgradeLevel(asset);

        

        if (savedLevel >= asset.costByLevel.Length)
        {

            this.level.text = savedLevel.ToString() +  " (Max)";
            buyButton.interactable = false;
            buyButton.transform.Find("Image (1)").gameObject.SetActive(false);
            buyButton.transform.Find("Text (TMP)").gameObject.SetActive(false);
            cost.text = "X";
            costNumber = int.MaxValue;
        }
        else
        {
            this.level.text = savedLevel.ToString();
            costNumber = asset.costByLevel[savedLevel];
            cost.text = asset.costByLevel[savedLevel].ToString();
            CheckCost(costNumber);
        }

        
    }

    public void Buy()
    {
        Initialize();
        Upgrades.BuyUpgrade(asset);
    }

    public void CheckCost(int money)
    {
        buyButton.interactable = money >= costNumber;
    }
}
