using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropDown : MonoBehaviour
{
    // Start is called before the first frame update

    public int output;

    void Start()
    {
        
    }

    public void HandleInputData(int val)
    {
        output = val;
    }

}
