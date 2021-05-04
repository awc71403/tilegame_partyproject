using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public static UIManager singleton;

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

    public GameObject skillUI;

    public GameObject levelUI;

    public TextMeshProUGUI floorText;
    public TextMeshProUGUI difficultyText;
    public TextMeshProUGUI enemiesText;

    public TextMeshProUGUI levelHealth;
    public TextMeshProUGUI levelAttack;
    public TextMeshProUGUI levelAbility;

    public Slider hpSlider;
    public TextMeshProUGUI hpText;

    public Button[] levelButton;
    public Button levelClose;

    public Image skill0;
    public Image skill1;
    public Image skill2;
    public Image skill3;

    public Image loadingPanel;
    public bool isLoading = true;

    // Start is called before the first frame update
    public void Awake()
    {
        if (singleton != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        singleton = this;
    }

    // Update is called once per frame
    void Update()
    {
        UIManager.singleton.HealthUI();
        if (isLoading) {
            return;
        }
        if (PlayerManager.singleton.GetCharacter().skillPoint > 0 && !levelUI.activeSelf) {
            OpenLevelMenu();
        }
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            menu.gameObject.SetActive(!menu.gameObject.activeSelf);
            if (menu.gameObject.activeSelf) {
                Time.timeScale = 0;
                isPaused = true;
                /*player = PlayerManager.singleton.gameObject.GetComponent<Character>();
                statNumbers = player.GetCopyStats();
                for (int x=0; x<statNumbers.Length; x++) {
                    Debug.Log(statNumbers[x]);
                    stats[x].text = stat_text[x] + statNumbers[x].ToString();
                }*/
            } else {
                Time.timeScale = 1;
                isPaused = false;
            }
        }
        
    }
    public void Loading() {
        isLoading = true;
        loadingPanel.gameObject.SetActive(true);
    }

    public void FinishedLoading() {
        GameManager.actionInProcess = false;    
        isLoading = false;
        loadingPanel.gameObject.SetActive(false);
    }

    public void UpdateUI()
    {
        floorText.text = $"Floor: {GameManager.floor}/100";
        difficultyText.text = $"Difficulty: {GameManager.difficulty}";
        enemiesText.text = $"Enemies: {GameManager.enemyCount}";
    }

    public void UpdateCD()
    {
        skillUI.SetActive(true);
        int[] cd = PlayerManager.singleton.GetCharacter().GetCurrentCD;
        skill0.gameObject.GetComponentInChildren<TextMeshProUGUI>(true).text = cd[0].ToString();
        if (cd[0] > 0)
        {
            skill0.gameObject.GetComponentInChildren<TextMeshProUGUI>(true).gameObject.SetActive(true);
            skill0.color = new Color(0, 0, 0, .5f);
        }
        else
        {
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

    public void HealthUI() {
        Character player = PlayerManager.singleton.GetCharacter();
        hpSlider.value = (float) player.currentHealth / (float)player.totalHealth;
        hpText.text = $"{player.currentHealth}/{player.totalHealth}";
    }

    public void exitGame() {
        Application.Quit();
    }
    public void btnChangeScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void UpdateLevelUI() {
        Character player = PlayerManager.singleton.GetCharacter();
        levelAbility.text = player.AbilityDmg.ToString();
        levelAttack.text = player.Attack.ToString();
        levelHealth.text = player.totalHealth.ToString();
    }

    public void UpgradeHealth() {
        PlayerManager.singleton.GetCharacter().levelUp(4);
        if (PlayerManager.singleton.GetCharacter().skillPoint <= 0)
        {
            foreach (Button button in levelButton)
            {
                button.gameObject.SetActive(false);
            }
            levelClose.gameObject.SetActive(true);
        }
    }

    public void UpgradeAttack()
    {
        PlayerManager.singleton.GetCharacter().levelUp(0);
        if (PlayerManager.singleton.GetCharacter().skillPoint <= 0)
        {
            foreach (Button button in levelButton)
            {
                button.gameObject.SetActive(false);
            }
            levelClose.gameObject.SetActive(true);
        }
    }
    public void UpgradeAbility()
    {
        PlayerManager.singleton.GetCharacter().levelUp(1);
        if (PlayerManager.singleton.GetCharacter().skillPoint <= 0) {
            foreach (Button button in levelButton)
            {
                button.gameObject.SetActive(false);
            }
            levelClose.gameObject.SetActive(true);
        }
    }

    private void OpenLevelMenu() {
        GameManager.actionInProcess = true;
        UpdateLevelUI();
        levelUI.SetActive(true);
        foreach (Button button in levelButton) {
            button.gameObject.SetActive(true);
        }
        levelClose.gameObject.SetActive(false);
    }

    public void CloseLevelMenu() {
        GameManager.actionInProcess = false;
        levelUI.SetActive(false);
    }

    public void SetAbilityImages(Sprite[] images) {
        skill0.sprite = images[0];
        skill1.sprite = images[1];
        skill2.sprite = images[2];
        skill3.sprite = images[3];
    }
}
