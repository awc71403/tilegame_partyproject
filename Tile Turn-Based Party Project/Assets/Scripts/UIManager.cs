using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public GameObject menu;
    public bool isPaused = false;
    public GameObject selectedTab;
    public GameObject[] buttons;
    public GameObject[] pages;
    public TMP_Text[] stats;
    private string[] stat_text = new string[] {"ATK: ", "Ability DMG: ", "Cooldown: ", "DEF: "};
    public Character player;
    public int[] statNumbers;
    public int[] playerAdjust;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            menu.gameObject.SetActive(!menu.gameObject.activeSelf);
            if (menu.gameObject.activeSelf) {
                Time.timeScale = 0;
                isPaused = true;
                player = PlayerManager.singleton.gameObject.GetComponent<Character>();
                statNumbers = player.GetCopyStats();
                for (int x=0; x<statNumbers.Length; x++) {
                    Debug.Log(statNumbers[x]);
                    stats[x].text = stat_text[x] + statNumbers[x].ToString();
                }
            } else {
                Time.timeScale = 1;
                isPaused = false;
            }
        }
        
    }
    public void exitGame() {
        Application.Quit();
    }
    public void btnChangeScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
