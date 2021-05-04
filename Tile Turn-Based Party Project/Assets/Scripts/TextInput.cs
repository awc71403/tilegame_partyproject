using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextInput : MonoBehaviour
{
    public string output;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void HandleInputData(string profile)
    {
        Debug.Log(profile);
        output = profile;
    }
}
