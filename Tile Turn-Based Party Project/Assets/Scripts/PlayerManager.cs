using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager singleton;

    public static PlayerManager GetSingleton()
    {
        return singleton;
    }

    private Character myCharacter;

    private string FLOOR = "floor";
    private string WALL = "wall";

    private const float stepDuration = 0.1f;

    // Start is called before the first frame update
    private void Awake()
    {
        if (singleton != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        singleton = this;

        myCharacter = GetComponent<Character>();
    }

    void Update() {
        if (!GameManager.actionInProcess) {
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.A)) {
                myCharacter.myDirection = Character.Direction.LEFT;
                Debug.Log(myCharacter.myDirection);
            }
            else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.S)) {
                myCharacter.myDirection = Character.Direction.DOWN;
                Debug.Log(myCharacter.myDirection);
            }
            else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.D)) {
                myCharacter.myDirection = Character.Direction.RIGHT;
                Debug.Log(myCharacter.myDirection);
            }
            else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.W)) {
                myCharacter.myDirection = Character.Direction.UP;
                Debug.Log(myCharacter.myDirection);
            }
            else if (Input.GetKeyDown(KeyCode.W)) {
                StartCoroutine(MoveUnitInDirection("up"));
            }
            else if (Input.GetKeyDown(KeyCode.A)) {
                StartCoroutine(MoveUnitInDirection("left"));
            }
            else if (Input.GetKeyDown(KeyCode.S)) {
                StartCoroutine(MoveUnitInDirection("down"));
            }
            else if (Input.GetKeyDown(KeyCode.D)) {
                StartCoroutine(MoveUnitInDirection("right"));
            }
            else if (Input.GetKeyDown(KeyCode.F)) {
                myCharacter.AttackEnemy();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1)) {
                myCharacter.Ability1();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2)) {
                myCharacter.Ability2();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3)) {
                myCharacter.Ability3();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4)) {
                myCharacter.Ability4();
            }
        }
    }

    #region Movement
    IEnumerator MoveUnitInDirection(string direction) {
        // Action in process!
        GameManager.actionInProcess = true;

        float tileSize = GetComponent<SpriteRenderer>().sprite.bounds.size.x;

        // Calculate the steps you need to take

        //Take that step!
        if (direction.Equals("up")) {
            myCharacter.transform.position += new Vector3(0, tileSize);
            TileBehavior upTile = myCharacter.occupiedTile.Up;
            if (upTile != null && upTile.tileType != WALL && !upTile.HasUnit()) {
                myCharacter.occupiedTile.ClearUnit();
                myCharacter.occupiedTile = upTile;
                myCharacter.myDirection = Character.Direction.UP;
            }
        }
        else if (direction.Equals("right")) {
            myCharacter.transform.position += new Vector3(tileSize, 0);
            TileBehavior rightTile = myCharacter.occupiedTile.Right;
            if (rightTile != null && rightTile.tileType != WALL && !rightTile.HasUnit()) {
                myCharacter.occupiedTile.ClearUnit();
                myCharacter.occupiedTile = rightTile;
                myCharacter.myDirection = Character.Direction.RIGHT;
            }
        }
        else if (direction.Equals("down")) {
            myCharacter.transform.position += new Vector3(0, -tileSize);
            TileBehavior downTile = myCharacter.occupiedTile.Down;
            if (downTile != null && downTile.tileType != WALL && !downTile.HasUnit()) {
                myCharacter.occupiedTile.ClearUnit();
                myCharacter.occupiedTile = downTile;
                myCharacter.myDirection = Character.Direction.DOWN;
            }
        }
        else if (direction.Equals("left")) {
            myCharacter.transform.position += new Vector3(-tileSize, 0);
            TileBehavior leftTile = myCharacter.occupiedTile.Left;
            if (leftTile != null && leftTile.tileType != WALL && !leftTile.HasUnit()) {
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

        // Action over!

        GameManager.UpdateEnemies();
        GameManager.actionInProcess = false;
    }
    #endregion

    #region Variable Functions

    public TileBehavior GetTile()
    {
        return myCharacter.occupiedTile;
    }


    public Character GetCharacter()
    {
        return myCharacter;
    }
    #endregion
}
