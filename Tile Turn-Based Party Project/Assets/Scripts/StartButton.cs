using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public string Get8CharacterRandomString()
    {
        string path = Path.GetRandomFileName();
        path = path.Replace(".", ""); // Remove period.
        return path.Substring(0, 8);  // Return 8 character string
    }

    public void StartGame()
    {
        string profile = GetComponent<TextInput>().output;
        int character = GetComponent<DropDown>().output;
        Debug.Log("Trying to start game");
        Debug.Log("profile " + profile);
        Debug.Log("character " + character);

        if (profile == string.Empty)
        {
            profile = Get8CharacterRandomString();
        }
        if (character == null)
        {
            character = 0;
        }
        PlayerPrefs.SetString("profile", profile); // Rudimentary
        PlayerPrefs.SetInt("character", character); // Rudimentary
        PlayerPrefs.Save();
        SceneManager.LoadScene("Game");
    }
}
