using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillStatusBar : MonoBehaviour
{
    public Character player;
    public Image fillImage;
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        player = PlayerManager.singleton.gameObject.GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        player = PlayerManager.singleton.gameObject.GetComponent<Character>();
        float fillValue = (float) player.currentHealth / player.totalHealth;
        slider.value = fillValue;
        if (fillValue <= 0) {
            fillImage.enabled = false;
        }
    }
}
