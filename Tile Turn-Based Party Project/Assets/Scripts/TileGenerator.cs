using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public int Type; //0 = floor, 1 = wall

    private Collider2D CollidedObj;

    public void SpawnCheck()
    {
        //collider check
        bool HitUp = Physics2D.OverlapCircle((Vector2)transform.position, 0.1f);
        if (HitUp)
        {
            Destroy(this.gameObject);
            return;
        }

        AllBlocksHandle Handle = GameObject.FindGameObjectWithTag("GameController").GetComponent<AllBlocksHandle>();

        if (Type == 0)
        {
            int spawnrand = UnityEngine.Random.Range(0, Handle.FloorTileBlocks.Length);
            GameObject bloc = Instantiate(Handle.FloorTileBlocks[spawnrand], transform.position, Quaternion.identity);
        }
        else if (Type == 1)
        {
            int spawnrand = UnityEngine.Random.Range(0, Handle.WallTileBlocks.Length);
            GameObject bloc = Instantiate(Handle.WallTileBlocks[spawnrand], transform.position, Quaternion.identity);
        }

        Destroy(this.gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
