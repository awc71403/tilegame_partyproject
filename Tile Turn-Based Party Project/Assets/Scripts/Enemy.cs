using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Character myCharacter;
    private GameObject player;

    private TileBehavior occupiedTile;
    private string FLOOR = "floor";
    private string WALL = "wall";

    public float viewDistance;

    void Start()
    {
        player = PlayerManager.GetSingleton().gameObject;
    }

    void Update()
    {
    }

    bool DetectPlayer()
    {
        Vector2 direction = player.transform.position - transform.position;
        List<RaycastHit2D> results = new List<RaycastHit2D>();
        ContactFilter2D contactFilter2D = new ContactFilter2D();
        int numHit = Physics2D.Raycast(transform.position, direction, contactFilter2D.NoFilter(), results, viewDistance);
        Debug.DrawRay(transform.position, direction.normalized * viewDistance, Color.red);

        bool blocked = false;
        bool playerVisible = false;

        for (int i = 0; i < numHit; i++)
        {
            GameObject hitObject = results[i].transform.gameObject;
            if (hitObject.GetComponent<PlayerManager>() != null)
            {
                if (!blocked)
                {
                    playerVisible = true;
                }
            }
            else if (hitObject.tag.Equals("Wall"))
            {
                blocked = true;
            }
        }

        return playerVisible;
    }

    public void Turn()
    {
        DetectPlayer();
        List<string> movementSteps = CalculateMovement(new List<string>(), originalTile, gameObject, unit.GetComponent<Character>().GetSpeed(), unitPlayer);
        MoveInDirection(movementSteps[0]);
    }

    void MoveInDirection(string direction)
    {
        float tileSize = GetComponent<SpriteRenderer>().sprite.bounds.size.x;

        if (direction.Equals("up"))
        {
            transform.position += new Vector3(0, tileSize);
            TileBehavior upTile = occupiedTile.Up;
            if (upTile != null && upTile.tileType != WALL && !upTile.HasUnit())
            {
                occupiedTile.ClearUnit();
                occupiedTile = upTile;
            }
        }
        else if (direction.Equals("right"))
        {
            transform.position += new Vector3(tileSize, 0);
            TileBehavior rightTile = occupiedTile.Right;
            if (rightTile != null && rightTile.tileType != WALL && !rightTile.HasUnit())
            {
                occupiedTile.ClearUnit();
                occupiedTile = rightTile;
            }
        }
        else if (direction.Equals("down"))
        {
            transform.position += new Vector3(0, -tileSize);
            TileBehavior downTile = occupiedTile.Down;
            if (downTile != null && downTile.tileType != WALL && !downTile.HasUnit())
            {
                occupiedTile.ClearUnit();
                occupiedTile = downTile;
            }
        }
        else if (direction.Equals("left"))
        {
            transform.position += new Vector3(-tileSize, 0);
            TileBehavior leftTile = occupiedTile.Left;
            if (leftTile != null && leftTile.tileType != WALL && !leftTile.HasUnit())
            {
                occupiedTile.ClearUnit();
                occupiedTile = leftTile;
            }
        }
        occupiedTile.PlaceUnit(myCharacter);
    }




    public List<string> CalculateMovement(List<string> movement, GameObject currentTile, GameObject goalTile, int moveEnergy, int unitPlayer)
    {
        // If you're there, return the movement path.
        if (currentTile.Equals(goalTile))
        {
            return movement;
        }

        // If you're out of energy, it's an invalid path.
        if (moveEnergy < 0)
        {
            return null;
        }

        List<List<string>> validPaths = new List<List<string>>();
        // Check for all adjacent tiles:
        Vector2[] directions = { Vector2.right, Vector2.left, Vector2.up, Vector2.down };
        foreach (Vector2 direction in directions)
        {
            RaycastHit2D hit = Physics2D.Raycast(currentTile.transform.position, direction, 1.0f);
            if (hit.collider != null && hit.transform.GetComponent<TileBehavior>().tileType != "wall")
            {
                GameObject otherTileUnit = hit.transform.GetComponent<TileBehavior>().myUnit;
                if (otherTileUnit == null || otherTileUnit.GetComponent<Character>().player == unitPlayer)
                {
                    List<string> newMovement = new List<string>(movement.ToArray());
                    if (direction.Equals(Vector2.right))
                    {
                        newMovement.Add("right");
                    }
                    if (direction.Equals(Vector2.left))
                    {
                        newMovement.Add("left");
                    }
                    if (direction.Equals(Vector2.up))
                    {
                        newMovement.Add("up");
                    }
                    if (direction.Equals(Vector2.down))
                    {
                        newMovement.Add("down");
                    }
                    List<string> path = CalculateMovement(newMovement, hit.collider.gameObject, goalTile, moveEnergy - currentTile.GetComponent<TileBehavior>().movementCost, unitPlayer);
                    if (path != null)
                    {
                        validPaths.Add(path);
                    }
                }
            }
        }

        // Return the shortest valid path
        if (validPaths.Count != 0)
        {
            List<string> shortestList = validPaths[0];
            foreach (List<string> path in validPaths)
            {
                if (path.Count < shortestList.Count)
                {
                    shortestList = path;
                }
            }
            return shortestList;
        }

        // If there are no valid paths from this point, return null
        return null;
    }


}
