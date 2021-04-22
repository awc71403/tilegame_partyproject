using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillStatusBar : MonoBehaviour
{
    public PlayerManager player;
    public Image fillImage;
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        player = PlayerManager.singleton;
        if (player) {
            Character character = player.GetComponent<Character>();
            float fillValue = (float)character.currentHealth / character.totalHealth;
            slider.value = fillValue;
            if (fillValue <= 0)
            {
                fillImage.enabled = false;
            }
        }
    }
}
