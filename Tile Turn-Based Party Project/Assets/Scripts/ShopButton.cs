using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
<<<<<<< Updated upstream
    public PlayerManager player;
    public Item item; 
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI priceLabel;
    public Image img; 
    public int price; 

    void Awake() {
        player = PlayerManager.singleton;
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
        player = PlayerManager.singleton;
        if (player.money - price >= 0) {
            player.money = player.money - price;
            DisableButton();
            Debug.Log("bought");
        }
        // add the item of the button to the inventory

        ShopManager.GetSingleton().CheckButtons();
    }

=======
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


>>>>>>> Stashed changes
    public void DisableButton() 
    {
        gameObject.GetComponent<Button>().interactable = false; 
    }
<<<<<<< Updated upstream


=======
    public void EnableButton() 
    {
        gameObject.GetComponent<Button>().interactable = true; 
    }
    public void SetPrices(int price) 
    {
        priceLabel.text = price.ToString();
    }
>>>>>>> Stashed changes
} 