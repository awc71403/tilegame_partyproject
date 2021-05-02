using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
<<<<<<< Updated upstream
=======
using UnityEngine.UI;
using TMPro;
>>>>>>> Stashed changes

public class ShopManager : MonoBehaviour
{
    public static ShopManager m_Singleton;
    public ShopButton button1;
    public ShopButton button2;
    public ShopButton button3;
<<<<<<< Updated upstream
    public Item[] items;
    public PlayerManager player; 

=======
    public ShopButton[] shopButtons;
    public TextMeshProUGUI balance; 

    public int price;
    
>>>>>>> Stashed changes
    public static ShopManager GetSingleton()
    {
        return ShopManager.m_Singleton;
    }
    void Awake() 
    {
        /**if (m_Singleton != null) {
            Debug.Log("Test");
            DestroyImmediate(gameObject);
            return;
        }
        */
        m_Singleton = this;
<<<<<<< Updated upstream
        player = PlayerManager.singleton;
=======
        SetButtons();
    }
    public void SetBalance() 
    {
        string money = PlayerManager.singleton.GetCharacter().money.ToString();
        balance.text = money;
    }
    void SetButtons() {
        shopButtons = new ShopButton[3];
        shopButtons[0] = button1;
        shopButtons[1] = button2;
        shopButtons[2] = button3; 
>>>>>>> Stashed changes
    }

    public void OpenShop()
    {
        gameObject.SetActive(true);
<<<<<<< Updated upstream
    }

    public void CloseShop() 
    {
        
        if (m_Singleton != null) {
            Debug.Log("Test");
            m_Singleton.gameObject.SetActive(false);
        }
        PlayerManager.GetSingleton().inShop = false;
        // move to the next area, this is just a placeholder. 
        // SceneManager.LoadScene(sceneName:"Camera-Following-Player");
    }

    /** Sets up all three buttons with the proper information. */
    public void SetButtons(Item[] i) 
    { 
        this.items = i; 
        button1.SetItem(items[0]);
        button2.SetItem(items[1]);
        button3.SetItem(items[2]);
        
    }

    public void CheckButtons()
    {
        player = PlayerManager.singleton;
        if (player.money - button1.item.price < 0) {
            button1.DisableButton();
        }
        if (player.money - button2.item.price < 0) {
            button2.DisableButton();
        }
        if (player.money - button3.item.price < 0) {
            button3.DisableButton();
        } 
    } 

=======
        SetPrices();
        CheckButtons();
        m_Singleton = this;
    }

    public void SetPrices() 
    {
        price = GetPrice();
        foreach (ShopButton button in shopButtons) {
            button.SetPrices(price);
        }
    }
    
    public int GetPrice() 
    {
        return GameManager.difficulty * 5;
    }
    public void CheckButtons()
    {   
        Character player = PlayerManager.singleton.GetCharacter();
        foreach (ShopButton button in shopButtons) {
            if (player.money - price < 0) {
                button.DisableButton();
            }
            else {
                button.EnableButton();
            }
        }
        SetBalance();
    } 

    public void CloseShop() {
        UIManager.singleton.CloseShop();
        if (GameManager.floor > 100)
        {
            Destroy(UIManager.singleton.gameObject);
            Destroy(PlayerManager.singleton.gameObject);
            SceneManager.LoadScene("Win");
        }
        else {
            UIManager.singleton.Loading();
            GameManager.IncreaseFloor();
            SceneManager.LoadScene("Game");
        }
    }


>>>>>>> Stashed changes

}
