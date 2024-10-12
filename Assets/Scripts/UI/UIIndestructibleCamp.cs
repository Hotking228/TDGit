using SpaceShooter;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIIndestructibleCamp : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private int price;

    private float timer;
    [SerializeField] private float indestructibleTime = 30f;
    [SerializeField] private Button button;


    private void Start()
    {
        TDPlayer.GoldUpdateSubscribe(GoldStatusCheck);
        priceText.text = price.ToString();
    }


    public void TryMakeIndestructible()
    {
        if (player.indestructible) return;
        if (TDPlayer.Instance.Gold < price) return;

        SetIndestructibleTimer();
        
        TDPlayer.Instance.ChangeGold(-price);
    }

    private void SetIndestructibleTimer()
    {
        timer = 0;
        player.indestructible = true;
        button.interactable = false;
    }
    private void Update()
    {
        if(!Player.Instance.indestructible)return;
        timer += Time.deltaTime;
        if (timer >= indestructibleTime)
        {
            player.indestructible = false;
        }
    }

    private void GoldStatusCheck(int gold)
    {
        if (gold >= price != button.interactable)
        {
            if (button == null) return;
            button.interactable = !button.interactable;
           
        }
        priceText.color = button.interactable ? Color.white : Color.red;
    }

}
