using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Start is called before the first frame update
    public int healingPoints;
    public int manaPoints;
    private Character myItem;
    public TileBehavior occupiedTile;
    void Awake()
    {
        myItem = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
