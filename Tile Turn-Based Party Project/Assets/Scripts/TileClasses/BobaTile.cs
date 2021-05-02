using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobaTile : TileBehavior
{
    // Start is called before the first frame update
    void Start()
    {
        tileType = "boba";
    }

    public override void Effect() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        int enemyCount = enemies.Length;
        for(int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<BasicEnemyAI>().Turn();
        }
    }
}
