using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllBlocksHandle : MonoBehaviour
{

    public GameObject[] LHall;
    public GameObject[] RHall;
    public GameObject[] UHall;
    public GameObject[] BHall;

    public GameObject[] LRooms;
    public GameObject[] RRooms;
    public GameObject[] URooms;
    public GameObject[] BRooms;
    public GameObject[] FloorTileBlocks;
    public GameObject[] WallTileBlocks;

    public GameObject StairTile;
    public GameObject GlassTile;
    public GameObject BobaTile;

    public int RoomSize;
 
    public GameObject PlayerPrefab;
    public GameObject[] Enemies;
    public GameObject[] Items;

    public int MaxEnemies = 10;
    public int MinEnemies = 6;
    private int enemiesToPlace;

    public int MaxGlass = 5;
    public int MinGlass = 2;

    public int MaxItems = 15;
    public int MinItems = 10;
    private int itemsToPlace;

    public int spawned = 0;
    public static AllBlocksHandle singleton;

    public LayerMask ActorLayer;

    // Start is called before the first frame update
    private void Awake()
    {
        if (singleton != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        singleton = this;
    }

    void Start()
    {
        enemiesToPlace = Random.Range(MinEnemies, MaxEnemies);
        itemsToPlace = Random.Range(MinItems, MaxItems);
        Invoke("Create", 2f);
        Invoke("Modify", 2.5f);
        Invoke("ReChecker", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Create()
    {
        GameObject[] Tiles = GameObject.FindGameObjectsWithTag("FloorTile");

        GameObject Tle = Tiles[Random.Range(0, Tiles.Length)];
        Collider2D CheckBlock = Physics2D.OverlapCircle(Tle.transform.position, 0.1f, ActorLayer);
        if (CheckBlock) {
            //can't place here
            Debug.Log("Other Character Already Placed");

        }
        else {
            //place enemy
            if (PlayerManager.singleton) 
            {
                Tle.GetComponent<TileBehavior>().PlaceUnit(PlayerManager.singleton.GetComponent<Character>());

            }
            else 
            { 
                GameObject Player = Instantiate(PlayerPrefab, Tle.transform.position, Quaternion.identity);
                Tle.GetComponent<TileBehavior>().PlaceUnit(Player.GetComponent<Character>());
                Vector3 pos = Player.transform.position;
                pos.z = -10;
                CameraManager.singleton.transform.position = pos;
            }

        }

        for (int i = 0; i < enemiesToPlace; i++)
        {
            //get random tile
            Tle = Tiles[Random.Range(0, Tiles.Length)];

            //check if tile has no other player/enemy
            CheckBlock = Physics2D.OverlapCircle(Tle.transform.position, 0.1f, ActorLayer);

            if (CheckBlock || Tle.GetComponent<TileBehavior>().HasUnit())
            {
                //can't place here
                Debug.Log("Other Character Already Placed");
        
            }
            else
            {
                //place enemy
                int randenemy = Random.Range(0, Enemies.Length);
                GameObject Enemy = Instantiate(Enemies[randenemy], Tle.transform.position, Quaternion.identity);
                Tle.GetComponent<TileBehavior>().PlaceUnit(Enemy.GetComponent<Character>());
                Debug.Log("Enemy Placed");
            }
        }

        for (int i = 0; i < itemsToPlace; i++)
        {
            //get random tile
            Tle = Tiles[Random.Range(0, Tiles.Length)];

            //check if tile has no other player/enemy
            CheckBlock = Physics2D.OverlapCircle(Tle.transform.position, 0.1f, ActorLayer);

            if (CheckBlock)
            {
                //can't place here
                Debug.Log("Item can't be place");

            }
            else
            {
                //place enemy
                int randItem = Random.Range(0, Items.Length);
                GameObject Item = Instantiate(Enemies[randItem], Tle.transform.position, Quaternion.identity);

                Debug.Log("Item Placed");
            }
        }
        GameManager.enemyCount = enemiesToPlace;
        UIManager.singleton.UpdateUI();
    }

    private void Modify() {
        GameObject[] Tiles = GameObject.FindGameObjectsWithTag("FloorTile");
        GameObject Tle = Tiles[Random.Range(0, Tiles.Length)];
        GameObject newTle;

        int glassToPlace = Random.Range(MinGlass, MaxGlass * GameManager.difficulty);
        for (int i = 0; i < glassToPlace; i++) {
            Tle = Tiles[Random.Range(0, Tiles.Length)];
            newTle = Instantiate(GlassTile);
            newTle.transform.position = Tle.transform.position;
            Destroy(Tle.gameObject);
        }

        Tle = Tiles[Random.Range(0, Tiles.Length)];
        newTle = Instantiate(StairTile);
        newTle.transform.position = Tle.transform.position;
        Destroy(Tle.gameObject);
    }

    private void ReChecker() {
        Debug.Log("ReChecker");
        GameObject[] Tiles = GameObject.FindGameObjectsWithTag("FloorTile");

        foreach (GameObject tileGameObject in Tiles)
        {
            TileBehavior tile = tileGameObject.GetComponent<TileBehavior>();

            if (tile)
            {
                Vector2[] directions = { Vector2.right, Vector2.left, Vector2.up, Vector2.down };

                foreach (Vector2 direction in directions)
                {
                    RaycastHit2D hit = Physics2D.Raycast(tile.gameObject.transform.position, direction, 1.0f);
                    if (hit.collider != null)
                    {
                        TileBehavior otherTile = hit.transform.GetComponent<TileBehavior>();
                        if (otherTile)
                        {
                            if (direction == Vector2.left)
                            {
                                tile.Left = otherTile;
                            }
                            else if (direction == Vector2.right)
                            {
                                tile.Right = otherTile;
                            }
                            else if (direction == Vector2.up)
                            {
                                tile.Up = otherTile;
                            }
                            else
                            {
                                tile.Down = otherTile;
                            }
                        }
                    }
                }
            }
        }

        UIManager.singleton.FinishedLoading();
    }

    public void SpawnEnemy() {
        spawned++;
        GameObject[] Tiles = GameObject.FindGameObjectsWithTag("FloorTile");

        GameObject Tle = Tiles[Random.Range(0, Tiles.Length)];

        if (Tle.GetComponent<TileBehavior>().HasUnit())
        {
            //can't place here
            Debug.Log("Other Character Already Placed");

        }
        else
        {
            GameObject Enemy;
            //Spawn oski
            if (spawned % 10 == 0)
            {
                int randenemy = Random.Range(0, Enemies.Length);
                Enemy = Instantiate(Enemies[randenemy], Tle.transform.position, Quaternion.identity);
            }
            //Spawn kiwi
            else {
                int randenemy = Random.Range(0, Enemies.Length);
                Enemy = Instantiate(Enemies[randenemy], Tle.transform.position, Quaternion.identity);
            }
            Tle.GetComponent<TileBehavior>().PlaceUnit(Enemy.GetComponent<Character>());
            Debug.Log("Enemy Placed");
        }
    }
}
