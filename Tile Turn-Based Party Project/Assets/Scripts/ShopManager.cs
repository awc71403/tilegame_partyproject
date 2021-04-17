using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    public GameObject shop;

    void Start() 
    {
        shop = GameManager.GetSingleton().shop;
    }

    public void OpenShop()
    {
        shop.SetActive(true);
    }

    public void CloseShop() 
    {
        shop.SetActive(false);
        PlayerManager.GetSingleton().inShop = false;
        // move to the next area, this is just a placeholder. 
        SceneManager.LoadScene(sceneName:"Camera-Following-Player");
    }
}
