using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitShop : MonoBehaviour
{
    public void ReturnToGame() {
        SceneManager.LoadScene(sceneName:"Camera-Following-Player");
    }
}
