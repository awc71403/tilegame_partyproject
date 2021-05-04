using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("HomeScreen");
    }
}
