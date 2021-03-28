using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratedBlockHandle : MonoBehaviour
{

    //creating another block set

    public TileGenerator[] TilesToGen;
    public RoomGenerator[] RoomsToGen;

    // Start is called before the first frame update
    void Start()
    {
        TilesToGen = GetComponentsInChildren<TileGenerator>();
        RoomsToGen = GetComponentsInChildren<RoomGenerator>();

        Invoke("StartGen", Random.Range(0.1f, 0.3f));
    }

    void StartGen()
    {
        foreach(TileGenerator tile in TilesToGen)
        {
            tile.SpawnCheck();
            tile.transform.parent = null;
        }

        foreach (RoomGenerator room in RoomsToGen)
        {
            room.SpawnCheck();
            room.transform.parent = null;
        }

        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
