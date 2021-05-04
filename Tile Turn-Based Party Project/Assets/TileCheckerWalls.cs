using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCheckerWalls : MonoBehaviour
{



    public GameObject LeftWall;
    public GameObject RightWall;
    public GameObject LowerWall;

    public GameObject DoubleLeftRightWall;
    public GameObject LeftUpRightSidedWall;

    public GameObject UpRightWallCorner;
    public GameObject UpLeftWallCorner;

    public GameObject UpRightSquare;
    public GameObject UpLeftSquare; 

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

    private void Start()
    {
        Invoke("Adjacency", 0.25f);
        Invoke("AssignDirection", 0.3f);
        
        
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



    void AssignDirection()
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

    }


    

    void CreateWalls()
    {

        TileBehavior tile = this.gameObject.GetComponent<TileBehavior>();

        // string lefttag = tile.Left.gameObject.tag;
        // string righttag = tile.Right.gameObject.tag;
        //string toptag = tile.Up.gameObject.tag;
        //string bottag = tile.Down.gameObject.tag;

        //string toprighttag = tile.UpRight.gameObject.tag;
        //string toplefttag = tile.UpLeft.gameObject.tag;
        //string botlefttag = tile.DownLeft.gameObject.tag;
        //string botrighttag = tile.DownRight.gameObject.tag; 

        Debug.Log(tile);
        Debug.Log(isB);
        Debug.Log(isL);
        

        if (isB == false)
        {
            
            if (isL == true && isR == true)
            {
                Debug.Log("isL and isR are true");


                Debug.Log(tile.Left != null);


                string lefttag = tile.Left.gameObject.tag;
                string righttag = tile.Right.gameObject.tag;

                Debug.Log("tags" + " " + lefttag);
                Debug.Log("tags" + " " + righttag);

                if (lefttag == "WallTile" && righttag == "WallTile")
                {
                    Instantiate(LowerWall, transform.position, Quaternion.identity);
                } 

            }

            /* else if (isR == true && isL == false && isT == true)
            {
                string righttag = tile.Right.gameObject.tag;
                string toptag = tile.Up.gameObject.tag;
                if (righttag == "WallTile" && toptag == "WallTile" )
                {
                    Instantiate(UpRightSquare, transform.position, Quaternion.identity);
                }
                
            } 
            */ 
            
        }

        

    }

}




