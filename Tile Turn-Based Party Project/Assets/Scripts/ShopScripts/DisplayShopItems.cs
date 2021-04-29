using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayShopItems : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.GetSingleton().gameObject;
        
    }

    // how do we know which items will be displayed? Where are they
} 
