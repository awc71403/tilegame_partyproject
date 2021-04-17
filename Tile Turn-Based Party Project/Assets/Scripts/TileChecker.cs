using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileChecker : MonoBehaviour
{
    public Sprite[] FloorTiles;
    public Sprite[] TwoSidedFloor;
    public Sprite[] TwoSidedFloorCorner;

    public LayerMask LayersToCheck;

    private bool isT;
    private bool isB;
    private bool isL;
    private bool isR;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("Adjacency", 0.5f);
        //Invoke("CheckTile", 0.5f);
    }

    void Adjacency() {
        TileBehavior tile = GetComponent<TileBehavior>();
        if (tile) {
            Vector2[] directions = { Vector2.right, Vector2.left, Vector2.up, Vector2.down };

            foreach (Vector2 direction in directions) {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1.0f);
                if (hit.collider != null) {
                    TileBehavior otherTile = hit.transform.GetComponent<TileBehavior>();
                    if (otherTile) {
                        if (direction == Vector2.left) {
                            tile.Left = otherTile;
                        }
                        else if (direction == Vector2.right) {
                            tile.Right = otherTile;
                        }
                        else if (direction == Vector2.up) {
                            tile.Up = otherTile;
                        }
                        else {
                            tile.Down = otherTile;
                        }
                    }
                }
            }
            
            // assign the diagonals
            // up left
            if (tile.Left != null) {
                if (tile.Left.Up != null) {
                    tile.UpLeft = tile.Left.Up;
                    tile.UpLeft.DownRight = tile;
                }
            }
            else if (tile.Up != null) {
                if (tile.Up.Left != null) {
                    tile.UpLeft = tile.Up.Left;
                    tile.UpLeft.DownRight = tile;
                }
            }

            // up right
            if (tile.Right != null) {
                if (tile.Right.Up != null) {
                    tile.UpRight = tile.Right.Up;
                    tile.UpRight.DownLeft = tile;
                }
            }

            else if (tile.Up != null) {
                if (tile.Up.Right != null) {
                    tile.UpRight = tile.Up.Right;
                    tile.UpRight.DownLeft = tile;
                }
            }

            // down left
            if (tile.Left != null) {
                if (tile.Left.Down != null) {
                    tile.DownLeft = tile.Left.Down;
                    tile.DownLeft.UpRight = tile;
                }
            }
            else if (tile.Down != null) {
                if (tile.Down.Left != null) {
                    tile.DownLeft = tile.Down.Left;
                    tile.DownLeft.UpRight = tile;
                }
            }
            
            // down right
            if (tile.Right != null) {
                if (tile.Right.Down != null) {
                    tile.DownRight = tile.Right.Down;
                    tile.DownRight.UpLeft = tile;
                }
            }
            else if (tile.Down != null) {
                if (tile.Down.Right != null) {
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

        SpriteRenderer FloorSpriteRenderer = GetComponent<SpriteRenderer>();
        FloorSpriteRenderer.sprite = FloorTiles[Random.Range(0, FloorTiles.Length)];

        
        if (isR && !isL && isT && isB) // left wall
        {
            FloorSpriteRenderer.sprite = TwoSidedFloor[0]; 
        }

        else if (!isR && isL && isT && isB) // right wall
        {
            FloorSpriteRenderer.sprite = TwoSidedFloor[1];
        }
        else if (isR && isL && !isT && isB) // top wall
        {
            FloorSpriteRenderer.sprite = TwoSidedFloor[2];
        }

        else if (isR && isL && isT && !isB) // bottom wall
        {
            FloorSpriteRenderer.sprite = TwoSidedFloor[3];
        }
        else if (isR && !isL && isT && !isB) //lower left
        {
            FloorSpriteRenderer.sprite = TwoSidedFloorCorner[0];
        }
        else if (isR && !isL && !isT && isB) //upper left
        {
            FloorSpriteRenderer.sprite = TwoSidedFloorCorner[1];
        }
        else if (!isR && isL && !isT && isB) //upper right 
        {
            FloorSpriteRenderer.sprite = TwoSidedFloorCorner[2];
        }
        else if(!isR && isL && isT && !isB) //lower right 
        {
            FloorSpriteRenderer.sprite = TwoSidedFloorCorner[3];
        }






    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
