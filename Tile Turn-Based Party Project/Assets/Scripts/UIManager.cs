using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject menu;
    public bool isPaused = false;
    public GameObject selectedTab;
    public List<GameObject> buttons;
    public List<GameObject> pages;
    // Start is called before the first frame update
    private void Awake()
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
            } else {
                Time.timeScale = 1;
                isPaused = false;
            }
        }
        
    }

}
