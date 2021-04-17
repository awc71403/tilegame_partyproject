using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager singleton;

    public static PlayerManager GetSingleton()
    {
        return singleton;
    }

    private Character myCharacter;
    public DirectionUI directionUI;
    public bool inShop;

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
        directionUI = GetComponentInChildren<DirectionUI>();
        inShop = false;
    }

    void Update() {

        if (!GameManager.actionInProcess) {
            if (myCharacter.currentHealth <= 0) {
                // Load a new scene the player dies
                SceneManager.LoadScene(sceneName:"Camera-Following-Player");
            }
            
            if (inShop) {
                // no input works if you are in shop    

            }
            
            else if (Input.GetKey(KeyCode.LeftShift)) {
                if (Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.A)) {
                    Debug.Log("upleft");
                    StartCoroutine(MoveUnitInDirection("upleft"));
                }
                if (Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.D)) {
                    Debug.Log("upright");
                    StartCoroutine(MoveUnitInDirection("upright"));
                }
                if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)) {
                    Debug.Log("downleft");
                    StartCoroutine(MoveUnitInDirection("downleft"));
                }
                if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)) {
                    Debug.Log("downright");
                    StartCoroutine(MoveUnitInDirection("downright"));
                    }
                }
            else if (Input.GetKey(KeyCode.LeftArrow)) {
                myCharacter.myDirection = Character.Direction.LEFT;
                Debug.Log(myCharacter.myDirection);
                myCharacter.setFlip(true);
                directionUI.SwitchDirection("left");
            }
            else if (Input.GetKey(KeyCode.DownArrow)) {
                myCharacter.myDirection = Character.Direction.DOWN;
                Debug.Log(myCharacter.myDirection);
                directionUI.SwitchDirection("down");
            }
            else if (Input.GetKey(KeyCode.RightArrow)) {
                myCharacter.myDirection = Character.Direction.RIGHT;
                Debug.Log(myCharacter.myDirection);
                myCharacter.setFlip(false);
                directionUI.SwitchDirection("right");
            }
            else if (Input.GetKey(KeyCode.UpArrow)) {
                myCharacter.myDirection = Character.Direction.UP;
                Debug.Log(myCharacter.myDirection);
                directionUI.SwitchDirection("up");
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
                myCharacter.occupiedTile.Effect();
                myCharacter.myDirection = Character.Direction.UP;
                directionUI.SwitchDirection("up");
            }
        }
        else if (direction.Equals("right")) {
            myCharacter.transform.position += new Vector3(tileSize, 0);
            TileBehavior rightTile = myCharacter.occupiedTile.Right;
            if (rightTile != null && rightTile.tileType != WALL && !rightTile.HasUnit()) {
                myCharacter.occupiedTile.ClearUnit();
                myCharacter.occupiedTile = rightTile;
                myCharacter.myDirection = Character.Direction.RIGHT;
                myCharacter.occupiedTile.Effect();
                myCharacter.setFlip(false);
                directionUI.SwitchDirection("right");
            }
        }
        else if (direction.Equals("down")) {
            myCharacter.transform.position += new Vector3(0, -tileSize);
            TileBehavior downTile = myCharacter.occupiedTile.Down;
            if (downTile != null && downTile.tileType != WALL && !downTile.HasUnit()) {
                myCharacter.occupiedTile.ClearUnit();
                myCharacter.occupiedTile = downTile;
                myCharacter.occupiedTile.Effect();                
                myCharacter.myDirection = Character.Direction.DOWN;
                directionUI.SwitchDirection("down");
            }
        }
        else if (direction.Equals("left")) {
            myCharacter.transform.position += new Vector3(-tileSize, 0);
            TileBehavior leftTile = myCharacter.occupiedTile.Left;
            if (leftTile != null && leftTile.tileType != WALL && !leftTile.HasUnit()) {
                myCharacter.occupiedTile.ClearUnit();
                myCharacter.occupiedTile = leftTile;
                myCharacter.myDirection = Character.Direction.LEFT;
                myCharacter.occupiedTile.Effect();
                myCharacter.setFlip(true);
                directionUI.SwitchDirection("left");
            }
        }
        else if (direction.Equals("upright")) {
            myCharacter.transform.position += new Vector3(-tileSize, 0);
            TileBehavior upright = myCharacter.occupiedTile.UpRight;
            if (upright != null && upright.tileType != WALL && !upright.HasUnit()) {
                myCharacter.occupiedTile.ClearUnit();
                myCharacter.occupiedTile = upright;
                myCharacter.myDirection = Character.Direction.UPRIGHT;
                myCharacter.occupiedTile.Effect();
                myCharacter.setFlip(false);
                directionUI.SwitchDirection("upright");
            }
        }
        else if (direction.Equals("upleft")) {
            myCharacter.transform.position += new Vector3(-tileSize, 0);
            TileBehavior upLeft = myCharacter.occupiedTile.UpLeft;
            if (upLeft != null && upLeft.tileType != WALL && !upLeft.HasUnit()) {
                myCharacter.occupiedTile.ClearUnit();
                myCharacter.occupiedTile = upLeft;
                myCharacter.myDirection = Character.Direction.UPLEFT;
                myCharacter.occupiedTile.Effect();
                myCharacter.setFlip(true);
                directionUI.SwitchDirection("upleft");
            }
        }
        else if (direction.Equals("downright")) {
            myCharacter.transform.position += new Vector3(-tileSize, 0);
            TileBehavior downRight = myCharacter.occupiedTile.DownRight;
            if (downRight != null && downRight.tileType != WALL && !downRight.HasUnit()) {
                myCharacter.occupiedTile.ClearUnit();
                myCharacter.occupiedTile = downRight;
                myCharacter.myDirection = Character.Direction.DOWNRIGHT;
                myCharacter.occupiedTile.Effect();
                myCharacter.setFlip(false);
                directionUI.SwitchDirection("downright");
            }
        }
        else if (direction.Equals("downleft")) {
            myCharacter.transform.position += new Vector3(-tileSize, 0);
            TileBehavior downLeft = myCharacter.occupiedTile.DownLeft;
            if (downLeft != null && downLeft.tileType != WALL && !downLeft.HasUnit()) {
                myCharacter.occupiedTile.ClearUnit();
                myCharacter.occupiedTile = downLeft;
                myCharacter.myDirection = Character.Direction.DOWNLEFT;
                myCharacter.occupiedTile.Effect();
                myCharacter.setFlip(true);
                directionUI.SwitchDirection("downleft");
            }
        }        
        myCharacter.updateCooldowns();
        myCharacter.RecalculateDepth();
        myCharacter.StartBounceAnimation();
        yield return new WaitForSeconds(stepDuration);
        myCharacter.occupiedTile.PlaceUnit(myCharacter);

        // Action over!
        GameManager.actionInProcess = false;
        GameManager.UpdateEnemies();
    }
    #endregion
}
