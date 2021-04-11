using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour
{

    private Character myCharacter;
    private GameObject player;

    private string FLOOR = "floor";
    private string WALL = "wall";


    private const float stepDuration = 0.1f;
    public float viewDistance;

    void Start()
    {
        player = PlayerManager.GetSingleton().gameObject;
    }

    private void Awake()
    {

        myCharacter = GetComponent<Character>();
    }

    void Update()
    {
    }

    //Return true if player is within line of sight
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
            else if (hitObject.GetComponent<TileBehavior>().tileType == "wall")
            {
                blocked = true;
            }
        }

        return playerVisible;
    }

    public void Turn()
    {
        //See if player is in line of sight
        if(DetectPlayer())
        {
            float tileSize = GetComponent<SpriteRenderer>().sprite.bounds.size.x;
            //Calculate path to player's tile from this tile
            List<string> steps = TileBehavior.CalculateMovement(new List<string>(), myCharacter.occupiedTile, PlayerManager.singleton.GetTile(), tileSize, viewDistance);
            
            //If there exists a path, take the first step towards it
            if (steps != null)
            {
                StartCoroutine(MoveInDirection(steps[0]));
            }
        }
    }

    IEnumerator MoveInDirection(string direction)
    {
        float tileSize = GetComponent<SpriteRenderer>().sprite.bounds.size.x;

        // Calculate the steps you need to take

        //Take that step!
        if (direction.Equals("up"))
        {
            myCharacter.transform.position += new Vector3(0, tileSize);
            TileBehavior upTile = myCharacter.occupiedTile.Up;
            if (upTile != null && upTile.tileType != WALL && !upTile.HasUnit())
            {
                myCharacter.occupiedTile.ClearUnit();
                myCharacter.occupiedTile = upTile;
                myCharacter.myDirection = Character.Direction.UP;
            }
        }
        else if (direction.Equals("right"))
        {
            myCharacter.transform.position += new Vector3(tileSize, 0);
            TileBehavior rightTile = myCharacter.occupiedTile.Right;
            if (rightTile != null && rightTile.tileType != WALL && !rightTile.HasUnit())
            {
                myCharacter.occupiedTile.ClearUnit();
                myCharacter.occupiedTile = rightTile;
                myCharacter.myDirection = Character.Direction.RIGHT;
            }
        }
        else if (direction.Equals("down"))
        {
            myCharacter.transform.position += new Vector3(0, -tileSize);
            TileBehavior downTile = myCharacter.occupiedTile.Down;
            if (downTile != null && downTile.tileType != WALL && !downTile.HasUnit())
            {
                myCharacter.occupiedTile.ClearUnit();
                myCharacter.occupiedTile = downTile;
                myCharacter.myDirection = Character.Direction.DOWN;
            }
        }
        else if (direction.Equals("left"))
        {
            myCharacter.transform.position += new Vector3(-tileSize, 0);
            TileBehavior leftTile = myCharacter.occupiedTile.Left;
            if (leftTile != null && leftTile.tileType != WALL && !leftTile.HasUnit())
            {
                myCharacter.occupiedTile.ClearUnit();
                myCharacter.occupiedTile = leftTile;
                myCharacter.myDirection = Character.Direction.LEFT;
            }
        }
        myCharacter.updateCooldowns();
        myCharacter.RecalculateDepth();
        myCharacter.StartBounceAnimation();
        yield return new WaitForSeconds(stepDuration);
        myCharacter.occupiedTile.PlaceUnit(myCharacter);

    }







}
