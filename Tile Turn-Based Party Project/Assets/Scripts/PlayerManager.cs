using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager singleton;

    public static PlayerManager GetSingleton()
    {
        return singleton;
    }

    private Character myCharacter;
    private PlayerData saved_data;

    private string profile = "tester";

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
        if (PlayerPrefs.HasKey(profile))
        {
            Debug.Log("getting save");
            saved_data = JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString(profile));
            if (saved_data != null && saved_data.HasCharacter(myCharacter))
            {
                Debug.Log("There is a save file with the selected character on it");
                CharacterData saved = saved_data.GetCharacterData(myCharacter);
                Debug.Log("everything is restored");
                myCharacter.curStatArr = saved.curStatArr;
                myCharacter.baseStats = saved.baseStats;
                myCharacter.currentCooldowns = saved.currentCooldowns;
                myCharacter.abilityCooldowns = saved.abilityCooldowns;
                myCharacter.abilityDurations = saved.abilityDurations;
                myCharacter.level = saved.level;
                myCharacter.totalHealth = saved.totalHealth;
                myCharacter.currentHealth = saved.currentHealth;
                myCharacter.experience = saved.experience;
            }
            else
            { // save new character to game
                Debug.Log("saving newly initiated character");
                saved_data.saveGame(myCharacter);
            }
        }
        else
        {
            Debug.Log("creating new save state");
            saved_data = new PlayerData(profile, myCharacter);
        }

        Debug.Log(JsonUtility.ToJson(saved_data));
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
            else if (Input.GetKeyDown(KeyCode.G)) // Save Character
            {
                Character new_char = myCharacter.saveCharacter();
                saved_data.saveGame(new_char);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha0)) 
            {
                if (saved_data != null)
                {
                    saved_data.saveGame(myCharacter);
                } else
                {
                    Debug.Log("savedata should exist...");
                }
                //Check if profile exists
                
            }
        }
    }

    #region Movement
    public IEnumerator MoveUnitInDirection(string direction) {
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
        GameManager.actionInProcess = false;
        GameManager.UpdateEnemies();
    }

    public void StartMoveDuringAttackAnimation()
    {
        StartCoroutine(MoveUnitInDirection(myCharacter.GetDirectionString()));
    }
}
    #endregion
