using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileChecker : MonoBehaviour
{
    public Sprite[] FloorTiles;
    public Sprite[] TwoSidedFloor;
    public Sprite[] TwoSidedFloorCorner;


    public GameObject GrayFloor;

    public GameObject[] TopWall;
    public GameObject LeftWall;
    public GameObject RightWall;
    public GameObject LowerWall;

    public GameObject DoubleLeftRightWall;
    public GameObject LeftUpRightSidedWall;

    public GameObject UpRightWallCorner;
    public GameObject UpLeftWallCorner;

    public LayerMask LayersToCheck;

    private bool isT;
    private bool isB;
    private bool isL;
    private bool isR;

    private bool isDoubleR;
    private bool isDoubleL;
    private bool isDoubleT;
    private bool isDoubleB;

    private bool isUL;
    private bool isUR;

    private bool isBL;
    private bool isBR;

    private bool currentPosition;




    // Start is called before the first frame update
    void Start()
    {
        Invoke("CheckTile", 0.5f);
        Invoke("Adjacency", 0.5f);
        Invoke("CreateWalls", 0.6f);



    }

    void Adjacency()
    {
        TileBehavior tile = GetComponent<TileBehavior>();


        if (tile)
        {
            Vector2[] directions = { Vector2.right, Vector2.left, Vector2.up, Vector2.down };

            foreach (Vector2 direction in directions)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1.0f);
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


            // up left
            if (tile.Left != null)
            {
                if (tile.Left.Up != null)
                {
                    tile.UpLeft = tile.Left.Up;
                    tile.UpLeft.DownRight = tile;
                }
            }
            else if (tile.Up != null)
            {
                if (tile.Up.Left != null)
                {
                    tile.UpLeft = tile.Up.Left;
                    tile.UpLeft.DownRight = tile;
                }
            }
            // up right
            if (tile.Right != null)
            {
                if (tile.Right.Up != null)
                {
                    tile.UpRight = tile.Right.Up;
                    tile.UpRight.DownLeft = tile;
                }
            }
            else if (tile.Up != null)
            {
                if (tile.Up.Right != null)
                {
                    tile.UpRight = tile.Up.Right;
                    tile.UpRight.DownLeft = tile;
                }
            }
            // down left
            if (tile.Left != null)
            {
                if (tile.Left.Down != null)
                {
                    tile.DownLeft = tile.Left.Down;
                    tile.DownLeft.UpRight = tile;
                }
            }
            else if (tile.Down != null)
            {
                if (tile.Down.Left != null)
                {
                    tile.DownLeft = tile.Down.Left;
                    tile.DownLeft.UpRight = tile;
                }
            }
            // down right
            if (tile.Right != null)
            {
                if (tile.Right.Down != null)
                {
                    tile.DownRight = tile.Right.Down;
                    tile.DownRight.UpLeft = tile;
                }
            }
            else if (tile.Down != null)
            {
                if (tile.Down.Right != null)
                {
                    tile.DownRight = tile.Down.Right;
                    tile.DownRight.UpLeft = tile;
                }
            }








        }
    }





    void CheckTile()
    {
        bool HitUp = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(0, 1), 0.1f, LayersToCheck);

        if (HitUp)
        {
            isT = true;
        }
        else
        {
            isT = false;
        }

        bool HitDown = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(0, -1), 0.1f, LayersToCheck);
        if (HitDown)
        {
            isB = true;

        }
        else
        {
            isB = false;
        }

        bool HitLeft = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(-1, 0), 0.1f, LayersToCheck);
        if (HitLeft)
        {
            isL = true;

        }
        else
        {
            isL = false;
        }

        bool HitRight = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(1, 0), 0.1f, LayersToCheck);
        if (HitRight)
        {
            isR = true;

        }
        else
        {
            isR = false;
        }

        bool HitRightRight = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(2, 0), 0.1f, LayersToCheck);
        if (HitRight)
        {
            isDoubleR = true;

        }
        else
        {
            isDoubleR = false;
        }

        bool HitLeftLeft = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(-2, 0), 0.1f, LayersToCheck);
        if (HitLeftLeft)
        {
            isDoubleL = true;

        }
        else
        {
            isDoubleL = false;
        }

        bool HitTopTop = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(0, 2), 0.1f, LayersToCheck);
        if (HitTopTop)
        {
            isDoubleT = true;

        }
        else
        {
            isDoubleT = false;
        }

        bool HitBotBot = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(0, -2), 0.1f, LayersToCheck);
        if (HitTopTop)
        {
            isDoubleB = true;

        }
        else
        {
            isDoubleB = false;
        }

        bool HitUpLeft = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(-1, 1), 0.1f, LayersToCheck);
        if (HitUpLeft)
        {
            isUL = true;

        }
        else
        {
            isUL = false;
        }


        bool HitUpRight = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(1, 1), 0.1f, LayersToCheck);
        if (HitUpRight)
        {
            isUR = true;

        }
        else
        {
            isUR = false;
        }

        bool HitBotRight = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(1, -1), 0.1f, LayersToCheck);
        if (HitBotRight)
        {
            isBR = true;

        }
        else
        {
            isBR = false;
        }

        bool HitBotLeft = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(-1, -1), 0.1f, LayersToCheck);
        if (HitBotLeft)
        {
            isBL = true;

        }
        else
        {
            isBL = false;
        }





        //SpriteRenderer FloorSpriteRenderer = GetComponent<SpriteRenderer>();
        //FloorSpriteRenderer.sprite = FloorTiles[Random.Range(0, FloorTiles.Length)];








    }




    void CreateWalls()
    {
        // create walls 
      

        if (isT == false) // upper wall
        {
            int number = Random.Range(0, TopWall.Length);
            Instantiate(TopWall[number], transform.position + new Vector3(0, 1), Quaternion.identity);
            CheckTile();
            Adjacency();
        }
        if (isR == false)
        {
            int number = Random.Range(0, TopWall.Length);
            Instantiate(TopWall[number], transform.position + new Vector3(1, 0), Quaternion.identity);
            CheckTile();
            Adjacency();
        }
        if (isB == false)
        {
            int number = Random.Range(0, TopWall.Length);
            Instantiate(TopWall[number], transform.position + new Vector3(0, -1), Quaternion.identity);
            CheckTile();
            Adjacency();
        }
        if (isL == false)
        {
            int number = Random.Range(0, TopWall.Length);
            Instantiate(TopWall[number], transform.position + new Vector3(-1, 0), Quaternion.identity);
            CheckTile();
            Adjacency();
        }
         if (isUL == false)
        {
            int number = Random.Range(0, TopWall.Length);
            Instantiate(TopWall[number], transform.position + new Vector3(-1, 1), Quaternion.identity);
            CheckTile();
            Adjacency();
        }

       if (isUR == false)
        {
            int number = Random.Range(0, TopWall.Length);
            Instantiate(TopWall[number], transform.position + new Vector3(1, 1), Quaternion.identity);
            CheckTile();
            Adjacency();
        }

        if (isBL == false)
        {
            int number = Random.Range(0, TopWall.Length);
            Instantiate(TopWall[number], transform.position + new Vector3(-1, -1), Quaternion.identity);
            CheckTile();
            Adjacency();
        }

        if (isBR == false)
        {
            int number = Random.Range(0, TopWall.Length);
            Instantiate(TopWall[number], transform.position + new Vector3(1, -1), Quaternion.identity);
            CheckTile();
            Adjacency();
        }

    }


    /* void CreateWalls2()
    {
        if (isB == false && isDoubleB == false && isBL == false && isBR == false) // lower wall
        {
            Instantiate(LowerWall, transform.position + new Vector3(0, -1), Quaternion.identity);

        }
        else if (isR == false && isDoubleR == true && isBR == false && isUR == false) // double side wall
        {
            if (!TopTileExists)
            {
                Instantiate(DoubleLeftRightWall, transform.position + new Vector3(1, 0), Quaternion.identity);
            }

        }
        else if (isL == false && isDoubleL == true && isBL == false && isUL == false) // double side wall
        {
            if (!TopTileExists)
            {
                Instantiate(DoubleLeftRightWall, transform.position + new Vector3(-1, 0), Quaternion.identity);
            }
        }
        else if (isB == false && isBL == true && isBR == true) // triple side wall left up right
        {
            Instantiate(LeftUpRightSidedWall, transform.position + new Vector3(0, -1), Quaternion.identity);

        }
    }

    public void CreateWalls3()
    {

        if (isBR == true && isR == true && isB == false) // upper right corner 
        {

            Instantiate(UpRightWallCorner, transform.position + new Vector3(0, -1), Quaternion.identity);

        }
        else if (isBL == true && isL == true && isB == false) // upper left corner
        {


            Instantiate(UpLeftWallCorner, transform.position + new Vector3(0, -1), Quaternion.identity);



        }
        else if (isL == false && isBL == false && isUL == false) // left wall 
        {
            if (!TopTileExists)
            {
                Instantiate(LeftWall, transform.position + new Vector3(-1, 0), Quaternion.identity);
            }
        }
        else if (isR == false && isBR == false && isUR == false && isDoubleR == false) // right wall
        {
            if (!TopTileExists)
            {
                Instantiate(RightWall, transform.position + new Vector3(1, 0), Quaternion.identity);
            }
        }

    }
     */ 















    // Update is called once per frame
    void Update()
    {

    }
}