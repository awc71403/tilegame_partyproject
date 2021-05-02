using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class StairsTile : TileBehavior
{
    public ShopManager shop;

    public void Start() {
        tileType = "stair";
        // get the shop somehow ?? 
        //shop = GameManager.GetSingleton().shop.GetComponent<ShopManager>();

    }
    
    public override void Effect() {
        //PlayerManager.GetSingleton().inShop = true;
        //shop.OpenShop();
<<<<<<< Updated upstream
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
=======
        UIManager.singleton.OpenShop();
>>>>>>> Stashed changes
    }

}
