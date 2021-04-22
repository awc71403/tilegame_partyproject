using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Collections;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string profile;
    public string monkJSON = "";
    public string catJSON = "";
    public PlayerData(string prof_name, Character current_char) 
    {
        profile = prof_name;
        saveCharacterData(current_char);

        string file_contents = JsonUtility.ToJson(this);
        Debug.Log(file_contents);

        PlayerPrefs.SetString(profile, file_contents);
        PlayerPrefs.Save();
        //serialize file to json
    }

    public void saveCharacterData(Character current_char)
    {
        if (current_char.characterName == "monk")
        {
            monkJSON = JsonUtility.ToJson(current_char);
        }
        else if (current_char.characterName == "cat")
        {
            catJSON = JsonUtility.ToJson(current_char);
        }
        Debug.Log("Character data saved");
    }

    public bool HasCharacter(Character current_char)
    {
        Debug.Log("current char char name" + current_char.characterName);
        if (current_char.characterName == "monk") { return monkJSON != string.Empty; }
        else if (current_char.characterName == "cat") { return catJSON != string.Empty; }
        return true;
    }

    public CharacterData GetCharacterData(Character current_char)
    {
        Debug.Log("we get here");
        Debug.Log(current_char.characterName);
        if (current_char.characterName == "monk") {
            Debug.Log("Turning json back to character");
            Debug.Log("monkJson : " + monkJSON);
            return JsonUtility.FromJson<CharacterData>(monkJSON);
        }
        else {
            Debug.Log("Turning json back to character");
            Debug.Log("catJSON : " + catJSON);
            return JsonUtility.FromJson<CharacterData>(catJSON);
        }
    }

    public void saveGame(Character character)
    {
        Debug.Log(character.characterName);
        saveCharacterData(character);
        string file_contents = JsonUtility.ToJson(this);
        Debug.Log(file_contents);
        PlayerPrefs.SetString(profile, file_contents);
        PlayerPrefs.Save();
    }
}
