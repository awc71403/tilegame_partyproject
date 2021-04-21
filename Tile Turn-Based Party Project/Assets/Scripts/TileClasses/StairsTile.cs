using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class StairsTile : TileBehavior
{
    public ShopManager shop;

    public void Start() {
        // get the shop somehow ?? 
        shop = GameManager.GetSingleton().shop.GetComponent<ShopManager>();

    }
    
    public override void Effect() {
        PlayerManager.GetSingleton().inShop = true;
        shop.OpenShop();
    }

}
