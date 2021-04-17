using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    public Character player;
    public Item item; 
    public GameObject button;
    public Text itemName;
    public Text priceLabel;
    public Image img; 
    // will have an item eventually

    void Start() {
        player = PlayerManager.singleton.gameObject.GetComponent<Character>();
    }

        // set the item text label, img, and itemName. 
    public void SetItem(Item i) {
        this.item = i;
        // this.itemName = i.name;
        // this.priceLabel = i.price;
        // this.img = i.image;
        // price = item.price;

    }
    public void Purchase() {

        /** if (player.money - item.price >= 0) {
            player.money = player.money - item.price;

            
            // add the item of the button to the inventory
        }
        */
    }

    public void SetActivity() {
        if (player != null) {
            Debug.Log("player is null");
            /** 
            if (player.money - price <= 0) {
            button.SetActive(false);
            }
        }
        */
        }
    }
} 