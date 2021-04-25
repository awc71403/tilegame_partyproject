using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager singleton;
    public GameObject menu;
    public bool isPaused = false;
    public GameObject selectedTab;
    public List<GameObject> buttons;
    public List<GameObject> pages;

    public GameObject skillUI;

    public TextMeshProUGUI floorText;
    public TextMeshProUGUI difficultyText;
    public TextMeshProUGUI enemiesText;

    public Image skill0;
    public Image skill1;
    public Image skill2;
    public Image skill3;

    // Start is called before the first frame update
    private void Awake()
    {
        if (singleton != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        singleton = this;
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
            } else {
                Time.timeScale = 1;
                isPaused = false;
            }
        }
        
    }

    public void UpdateUI() {
        floorText.text = $"Floor: {GameManager.floor}/100";
        difficultyText.text = $"Difficulty: {GameManager.difficulty}";
        enemiesText.text = $"Enemies: {GameManager.enemyCount}";
    }

    public void UpdateCD() {
        skillUI.SetActive(true);
        int[] cd = PlayerManager.singleton.GetCharacter().GetCurrentCD;
        skill0.gameObject.GetComponentInChildren<TextMeshProUGUI>(true).text = cd[0].ToString();
        if (cd[0] > 0)
        {
            skill0.gameObject.GetComponentInChildren<TextMeshProUGUI>(true).gameObject.SetActive(true);
            skill0.color = new Color(0, 0, 0, .5f);
        }
        else {
            skill0.gameObject.GetComponentInChildren<TextMeshProUGUI>(true).gameObject.SetActive(false);
            skill0.color = new Color(1, 1, 1);
        }
        skill1.gameObject.GetComponentInChildren<TextMeshProUGUI>(true).text = cd[1].ToString();
        if (cd[1] > 0)
        {
            skill1.gameObject.GetComponentInChildren<TextMeshProUGUI>(true).gameObject.SetActive(true);
            skill1.color = new Color(0, 0, 0, .5f);
        }
        else
        {
            skill1.gameObject.GetComponentInChildren<TextMeshProUGUI>(true).gameObject.SetActive(false);
            skill1.color = new Color(1, 1, 1);
        }
        skill2.gameObject.GetComponentInChildren<TextMeshProUGUI>(true).text = cd[2].ToString();
        if (cd[2] > 0)
        {
            skill2.gameObject.GetComponentInChildren<TextMeshProUGUI>(true).gameObject.SetActive(true);
            skill2.color = new Color(0, 0, 0, .5f);
        }
        else
        {
            skill2.gameObject.GetComponentInChildren<TextMeshProUGUI>(true).gameObject.SetActive(false);
            skill2.color = new Color(1, 1, 1);
        }
        skill3.gameObject.GetComponentInChildren<TextMeshProUGUI>(true).text = cd[3].ToString();
        if (cd[3] > 0)
        {
            skill3.gameObject.GetComponentInChildren<TextMeshProUGUI>(true).gameObject.SetActive(true);
            skill3.color = new Color(0, 0, 0, .5f);
        }
        else
        {
            skill3.gameObject.GetComponentInChildren<TextMeshProUGUI>(true).gameObject.SetActive(false);
            skill3.color = new Color(1, 1, 1);
        }
    }

}
