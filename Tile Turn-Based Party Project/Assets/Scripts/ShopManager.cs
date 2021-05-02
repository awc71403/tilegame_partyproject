using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public static ShopManager m_Singleton;
    public ShopButton button1;
    public ShopButton button2;
    public ShopButton button3;

    public ShopButton[] shopButtons;
    public TextMeshProUGUI balance; 

    public int price;

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
    }

    public void SetButtons() {
        shopButtons = new ShopButton[3];
        shopButtons[0] = button1;
        shopButtons[1] = button2;
        shopButtons[2] = button3;
    }
    public void SetBalance() 
    {
        string money = PlayerManager.singleton.GetCharacter().money.ToString();
        balance.text = money;
    }

    public void OpenShop()
    {
        SetButtons();
        gameObject.SetActive(true);
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

}
