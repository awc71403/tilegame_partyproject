using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    public Character player;
    public Item item; 
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI priceLabel;
    public Image img; 
    public int price; 

    void Awake() {
        player = PlayerManager.singleton.gameObject.GetComponent<Character>();
    }

        // set the item text label, img, and itemName. 
    public void SetItem(Item i) 
    {
        this.item = i;
        this.itemName.SetText(i.name);
        this.priceLabel.SetText(i.price.ToString());
        // this.img = i.image;
        if (player.money - item.price <= 0) 
        {
            DisableButton(); 
        }

    }
    public void Purchase() 
    {
        player = PlayerManager.singleton.gameObject.GetComponent<Character>();
        if (player.money - price >= 0) {
            player.money = player.money - price;
            DisableButton();
            Debug.Log("bought");
        }
        // add the item of the button to the inventory

        ShopManager.GetSingleton().CheckButtons();
    }

    public void DisableButton() 
    {
        gameObject.GetComponent<Button>().interactable = false; 
    }


} 