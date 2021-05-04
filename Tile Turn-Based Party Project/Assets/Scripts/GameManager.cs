using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Variables
    private static GameManager m_Singleton;
    public static GameManager GetSingleton() {
        return m_Singleton;
    }

    public static bool actionInProcess;

    public static int difficulty;

    [SerializeField]
    GameObject testEnemy;

    [SerializeField]
    GameObject testItem;

    [SerializeField]
    public ShopManager shop; 

    TileBehavior[,] mapArray;
        float tileSize;

    [SerializeField]
    private string profile;

    [SerializeField]
    private string testFile;

    public static int enemyCount = 0;
    public static int floor = 1;
    private static int turn = 0;

    public static int tries = 0;
    public static int highscore;
    #endregion

    #region Initialization
    public void Awake() {
        // Singleton makes sure there is only one of this object
        if (m_Singleton != null) {
            DestroyImmediate(gameObject);
            return;
        }
        m_Singleton = this;

        if (PlayerPrefs.HasKey("Highscore")) {
            highscore = PlayerPrefs.GetInt("Highschore");
        }

        UpdateDifficulty();
    }

    public void Start() {
        // FOR TESTING PURPOSES
        profile = PlayerPrefs.GetString("profile");
        int c = PlayerPrefs.GetInt("character");
        
    }

    public void Update() {
        
    }
    #endregion

    public static void Reset() {

        tries++;
        floor = 1;
        turn = 0;
        UpdateDifficulty();
    }

    public static void UpdateEnemies()
    {
        turn++;
        if (turn % 5 == 0)
        {
            AllBlocksHandle.singleton.SpawnEnemy();
        }
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = enemies.Length;
        for(int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<BasicEnemyAI>().Turn();
        }
        UpdateDifficulty();
        UIManager.singleton.UpdateUI();
    }

    #region UI
    public void ShowCharacterUI(Character selectedUnit) {

    }

    public void ClearUI() {

    }
    #endregion

    public static void IncreaseFloor() {
        floor++;
        UpdateDifficulty();
    }

    public static void UpdateDifficulty() {
        difficulty = (turn / 50) + floor;
    }

    public static void Win()
    {
        PlayerPrefs.SetInt("Highscore", Mathf.Max(tries, highscore));
    }

}
