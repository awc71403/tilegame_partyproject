﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    private PlayerData saved_data;

    public int money;

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

        DontDestroyOnLoad(gameObject);
        singleton = this;
        inShop = false; 
        myCharacter = GetComponent<Character>();
        directionUI = GetComponentInChildren<DirectionUI>();
        if (PlayerPrefs.HasKey(profile))
        {
            Debug.Log("getting save");
            saved_data = JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString(profile));
            if (saved_data != null && saved_data.HasCharacter(myCharacter) && saved_data.GetCharacterData(myCharacter) != null)
            {
                Debug.Log("There is a save file with the selected character on it");
                CharacterData saved = saved_data.GetCharacterData(myCharacter);
                Debug.Log("everything is restored");
                Debug.Log(myCharacter);
                Debug.Log(saved);
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

    private void Start()
    {
        UIManager.singleton.UpdateCD();
    }

    void Update() {

        if (!GameManager.actionInProcess) { 
            if (myCharacter.currentHealth <= 0) {
                // Load a new scene the player dies
                GameManager.Reset();
                myCharacter.ResetHealth();
                UIManager.singleton.Loading();
                SceneManager.LoadScene(sceneName:"Game");
            }
            
            if (inShop) {
                // no input works if you are in shop    
                return;
            }
            
            /*else if (Input.GetKey(KeyCode.LeftShift)) {
                if ((Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.A)) || (Input.GetKeyDown(KeyCode.W) && Input.GetKey(KeyCode.A))) {
                    Debug.Log("upleft");
                    StartCoroutine(MoveUnitInDirection("upleft"));
                }
                if ((Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.D)) || (Input.GetKeyDown(KeyCode.W) && Input.GetKey(KeyCode.D))) {
                    Debug.Log("upright");
                    StartCoroutine(MoveUnitInDirection("upright"));
                }
                if ((Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.A)) || (Input.GetKeyDown(KeyCode.S) && Input.GetKey(KeyCode.A))) {
                    Debug.Log("downleft");
                    StartCoroutine(MoveUnitInDirection("downleft"));
                }
                if ((Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.D)) || (Input.GetKeyDown(KeyCode.S ) && Input.GetKey(KeyCode.D))) {
                    Debug.Log("downright");
                    StartCoroutine(MoveUnitInDirection("downright"));
                    }
                }*/
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
                TileBehavior upTile = myCharacter.occupiedTile.Up;
                if (upTile != null && upTile.tileType != WALL && !upTile.HasUnit())
                {
                    StartCoroutine(MoveUnitInDirection("up"));
                }
            }
            else if (Input.GetKeyDown(KeyCode.A)) {
                TileBehavior leftTile = myCharacter.occupiedTile.Left;
                if (leftTile != null && leftTile.tileType != WALL && !leftTile.HasUnit())
                {
                    StartCoroutine(MoveUnitInDirection("left"));
                }
            }
            else if (Input.GetKeyDown(KeyCode.S)) {
                TileBehavior downTile = myCharacter.occupiedTile.Down;
                if (downTile != null && downTile.tileType != WALL && !downTile.HasUnit())
                {
                    StartCoroutine(MoveUnitInDirection("down"));
                }
            }
            else if (Input.GetKeyDown(KeyCode.D)) {
                TileBehavior rightTile = myCharacter.occupiedTile.Right;
                if (rightTile != null && rightTile.tileType != WALL && !rightTile.HasUnit())
                {
                    StartCoroutine(MoveUnitInDirection("right"));
                }
            }
            else if (Input.GetKeyDown(KeyCode.F)) {
                myCharacter.AttackEnemy();
                GameManager.UpdateEnemies();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1)) {
                myCharacter.Ability1();
                GameManager.UpdateEnemies();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2)) {
                myCharacter.Ability2();
                GameManager.UpdateEnemies();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3)) {
                myCharacter.Ability3();
                GameManager.UpdateEnemies();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4)) {
                myCharacter.Ability4();
                GameManager.UpdateEnemies();
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
            myCharacter.occupiedTile.ClearUnit();
            myCharacter.occupiedTile = upTile;
            myCharacter.occupiedTile.Effect();
            myCharacter.myDirection = Character.Direction.UP;
            directionUI.SwitchDirection("up");
        }
        else if (direction.Equals("right")) {
            myCharacter.transform.position += new Vector3(tileSize, 0);
            TileBehavior rightTile = myCharacter.occupiedTile.Right;
            myCharacter.occupiedTile.ClearUnit();
            myCharacter.occupiedTile = rightTile;
            myCharacter.myDirection = Character.Direction.RIGHT;
            myCharacter.occupiedTile.Effect();
            myCharacter.setFlip(false);
            directionUI.SwitchDirection("right");
        }
        else if (direction.Equals("down")) {
            myCharacter.transform.position += new Vector3(0, -tileSize);
            TileBehavior downTile = myCharacter.occupiedTile.Down;
            myCharacter.occupiedTile.ClearUnit();
            myCharacter.occupiedTile = downTile;
            myCharacter.occupiedTile.Effect();                
            myCharacter.myDirection = Character.Direction.DOWN;
            directionUI.SwitchDirection("down");
        }
        else if (direction.Equals("left")) {
            myCharacter.transform.position += new Vector3(-tileSize, 0);
            TileBehavior leftTile = myCharacter.occupiedTile.Left;
            myCharacter.occupiedTile.ClearUnit();
            myCharacter.occupiedTile = leftTile;
            myCharacter.myDirection = Character.Direction.LEFT;
            myCharacter.occupiedTile.Effect();
            myCharacter.setFlip(true);
            directionUI.SwitchDirection("left");
        }
        /*else if (direction.Equals("upright")) {
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
        }*/        
        myCharacter.updateCooldowns();
        myCharacter.RecalculateDepth();
        myCharacter.StartBounceAnimation();
        yield return new WaitForSeconds(stepDuration);
        myCharacter.occupiedTile.PlaceUnit(myCharacter);
        if (myCharacter.occupiedTile.tileType == "glass")
        {
            myCharacter.MakeGlassSound();
        }
        else {
            myCharacter.MakeStepSound();
        }

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

    public void StartMoveDuringAttackAnimation()
    {
        StartCoroutine(MoveUnitInDirection(myCharacter.GetDirectionString()));
    }
}
    #endregion
