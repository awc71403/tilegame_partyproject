using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    public static ShopManager m_Singleton;
    public ShopButton button1;
    public ShopButton button2;
    public ShopButton button3;
    public Item[] items;
    public Character player; 

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
        player = PlayerManager.singleton.gameObject.GetComponent<Character>();
    }

    public void OpenShop()
    {
        gameObject.SetActive(true);
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
        player = PlayerManager.singleton.gameObject.GetComponent<Character>();
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


}
