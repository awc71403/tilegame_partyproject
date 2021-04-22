using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{

    private Collider2D CollidedObj;

    public int direction = 0; // 0 1 2 3, left right up down

    public int CreateHall; // 0 = no, 1 = yes
    
    public void SpawnCheck()
    {
        //collider check
        CollidedObj = Physics2D.OverlapCircle((Vector2)transform.position, 0.2f);

        if (CollidedObj)
        {
            Destroy(this.gameObject);
            return;
        }

        //spawn room block
        AllBlocksHandle Handle = GameObject.FindGameObjectWithTag("GameController").GetComponent<AllBlocksHandle>();
      
        if (Handle.RoomSize <= 0)
        {
            Destroy(this.gameObject);
            return;
        
        }

        GameObject bloc = null;

        if (CreateHall == 0)
        {
            if (direction == 0)
            {
                int spawnrand = Random.Range(0, Handle.LRooms.Length);
                bloc = Instantiate(Handle.LRooms[spawnrand], transform.position, Quaternion.identity);
                Debug.Log("left room");
            }
            else if (direction == 1)
            {
                int spawnrand = Random.Range(0, Handle.RRooms.Length);
                bloc = Instantiate(Handle.RRooms[spawnrand], transform.position, Quaternion.identity);
                Debug.Log("right room");
            }
            else if (direction == 2)
            {
                int spawnrand = Random.Range(0, Handle.URooms.Length);
                bloc = Instantiate(Handle.URooms[spawnrand], transform.position, Quaternion.identity);
                Debug.Log("top room");
            }
            else
            {
                int spawnrand = Random.Range(0, Handle.BRooms.Length);
                bloc = Instantiate(Handle.BRooms[spawnrand], transform.position, Quaternion.identity);
                Debug.Log("bot room");
            }
            
        }
        else
        {
            if (direction == 0)
            {
                int spawnrand = Random.Range(0, Handle.LHall.Length);
                bloc = Instantiate(Handle.LHall[spawnrand], transform.position, Quaternion.identity);
                Debug.Log("left hall");
            }
            else if (direction == 1)
            {
                int spawnrand = Random.Range(0, Handle.RHall.Length);
                bloc = Instantiate(Handle.RHall[spawnrand], transform.position, Quaternion.identity);
                Debug.Log("right hall");
            }
            else if (direction == 2)
            {
                int spawnrand = Random.Range(0, Handle.UHall.Length);
                bloc = Instantiate(Handle.UHall[spawnrand], transform.position, Quaternion.identity);
                Debug.Log("top hall");
            }
            else
            {
                int spawnrand = Random.Range(0, Handle.BHall.Length);
                bloc = Instantiate(Handle.BHall[spawnrand], transform.position, Quaternion.identity);
                Debug.Log("bot hall");
            }
           
        }


        Handle.RoomSize -= 1;
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
