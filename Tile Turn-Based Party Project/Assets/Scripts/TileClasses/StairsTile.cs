using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StairsTile : TileBehavior
{
    public override void Effect() {
        SceneManager.LoadScene(sceneName:"Camera-Following-Player");
    }
}
