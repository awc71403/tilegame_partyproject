using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    public Character player;
    public TextMeshProUGUI priceLabel;

    void Awake() {
        player = PlayerManager.singleton.GetCharacter();
    }

    public void Purchase() 
    {
        player = PlayerManager.singleton.GetCharacter();
        int price = ShopManager.GetSingleton().price;
        if (player.money - price >= 0) {
            player.money = player.money - price;
            // might want to change how much the boba heals
            player.currentHealth += price;
        }
        DisableButton();
        ShopManager.GetSingleton().CheckButtons();
    }


    public void DisableButton() 
    {
        gameObject.GetComponent<Button>().interactable = false; 
    }
    public void EnableButton() 
    {
        gameObject.GetComponent<Button>().interactable = true; 
    }
    public void SetPrices(int price) 
    {
        priceLabel.text = price.ToString();
    }
} 